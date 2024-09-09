using MongoDB.Driver;
using RabbitMQ.Client;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Constants.MessageBrokers.UriAndUrl;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.OutboxPublishers.CarOutboxPublisher;
using RentACarNow.Common.Infrastructure.Extensions;
var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddHostedService<CarPublisherService>();


builder.Services.AddSingleton<MongoCarOutboxContext>(x =>
{
    return new MongoCarOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
});

builder.Services.AddSingleton<ICarOutboxRepository, CarOutboxMongoRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "CarOutboxPublisher");
var host = builder.Build();

host.Services.GetService<IRabbitMQMessageService>()?.CreateQueues();
host.Services.GetService<IRabbitMQMessageService>()?.CreateExchanges();
host.Services.GetService<IRabbitMQMessageService>()?.BindExchangesAndQueues();


host.Run();
