using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.OutboxPublishers.UserOutboxPublisher;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Services.Interfaces;


var builder = Host.CreateApplicationBuilder(args);



builder.Services.AddSingleton<MongoUserOutboxContext>(x =>
{
    return new MongoUserOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
});

builder.Services.AddHostedService<UserPublisherService>();

builder.Services.AddSingleton<IUserOutboxRepository, UserOutboxMongoRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "useroutboxpublisher");

var host = builder.Build();

host.Services.GetService<IRabbitMQMessageService>()?.CreateExchanges();
host.Services.GetService<IRabbitMQMessageService>()?.CreateQueues();
host.Services.GetService<IRabbitMQMessageService>()?.BindExchangesAndQueues();



host.Run();
