using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.OutboxPublishers.ClaimOutboxPublisher;
using RentACarNow.Common.Infrastructure.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ClaimPublisherService>();


builder.Services.AddSingleton<MongoClaimOutboxContext>(x =>
{
    return new MongoClaimOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
});


builder.Services.AddSingleton<IClaimOutboxRepository, ClaimOutboxMongoRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "brandoutboxpublisher");


var host = builder.Build();
host.Run();
