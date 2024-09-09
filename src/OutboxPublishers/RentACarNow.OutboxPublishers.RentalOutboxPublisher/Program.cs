
using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.OutboxPublishers.RentalOutboxPublisher;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

var builder = Host.CreateApplicationBuilder(args);



builder.Services.AddSingleton<MongoRentalOutboxContext>(x =>
{
    return new MongoRentalOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
});

builder.Services.AddHostedService<RentalPublisherService>();

builder.Services.AddSingleton<IRentalOutboxRepository, RentalOutboxMongoRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "RentalOutboxPublisher");

var host = builder.Build();
host.Services.GetService<IRabbitMQMessageService>()?.CreateExchanges();
host.Services.GetService<IRabbitMQMessageService>()?.CreateQueues();
host.Services.GetService<IRabbitMQMessageService>()?.BindExchangesAndQueues();




host.Run();
