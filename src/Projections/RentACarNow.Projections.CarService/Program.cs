using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.InboxContexts.Implementations;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Projections.CarService;
using RentACarNow.Projections.CarService.Consumers;
using RentACarNow.Common.Infrastructure.Services;
using RentACarNow.Common.Infrastructure.Extensions;
var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddHostedService<ProjectionService>();
builder.Services.AddHostedService<CarCreatedEventConsumer>();
builder.Services.AddHostedService<CarDeletedEventConsumer>();
builder.Services.AddHostedService<CarUpdatedEventConsumer>();
builder.Services.AddHostedService<CarFeatureAddedEventConsumer>();
builder.Services.AddHostedService<CarFeatureDeletedEventConsumer>();
builder.Services.AddHostedService<CarFeatureUpdatedEventConsumer>();


builder.Services.AddSingleton<MongoCarInboxContext>(p =>
{
    return new MongoCarInboxContext(
    mongoClient: new MongoClient(MongoDbConstants.CONNECTION_STRING),
    databaseName: "InboxDB");
});

builder.Services.AddSingleton<IBrandInboxRepository, BrandInboxMongoRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "CarProjectionService");

var host = builder.Build();
host.Run();
