using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.InboxContexts.Implementations;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Projections.ClaimService;
using RentACarNow.Projections.ClaimService.Consumers;
using System.Reflection;
using RentACarNow.Common.Infrastructure.Extensions;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddHostedService<ProjectionService>();


builder.Services.AddHostedService<ClaimDeletedEventConsumer>();
builder.Services.AddHostedService<ClaimAddedEventConsumer>();
builder.Services.AddHostedService<ClaimUpdatedEventConsumer>();

builder.Services.AddSingleton<MongoClaimInboxContext>(p =>
{
    return new MongoClaimInboxContext(
    mongoClient: new MongoClient(MongoDbConstants.CONNECTION_STRING),
    databaseName: "InboxDB");
});

builder.Services.AddSingleton<IClaimnboxRepository, ClaimInboxMongoRepository>();


builder.Services.AddMongoRentalACarNowDBContext();
builder.Services.AddSingleton<IMongoClaimWriteRepository, MongoClaimWriteRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "ClaimProjectionService");

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



var host = builder.Build();
host.Run();
