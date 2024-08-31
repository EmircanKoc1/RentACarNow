using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.OutboxPublishers.BrandOutboxPublisher;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton<MongoBrandOutboxContext>(x =>
{
    return new MongoBrandOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
});

builder.Services.AddHostedService<BrandPublisherService>();

builder.Services.AddSingleton<IBrandOutboxRepository, BrandOutboxMongoRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "brandoutboxpublisher");

var host = builder.Build();
host.Run();
