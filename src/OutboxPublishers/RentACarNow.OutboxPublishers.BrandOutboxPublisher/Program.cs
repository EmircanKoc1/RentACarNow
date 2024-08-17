
using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.OutboxPublishers.BrandOutboxPublisher.PublisherServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IBrandOutboxRepository, BrandOutboxMongoRepository>(provider =>
{
    return new BrandOutboxMongoRepository(new MongoBrandOutboxContext(
        new MongoClient(MongoDbConstants.CONNECTION_STRING), "BrandOutboxDB"));

});
builder.Services.AddHostedService<BrandOutboxPublisherBGService>();


var host = builder.Build();
host.Run();
