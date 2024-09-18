using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.InboxContexts.Implementations;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Projections.RentalService;
using RentACarNow.Projections.RentalService.Consumers;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ProjectionService>();

builder.Services.AddHostedService<RentalAddedEventConsumer>();
builder.Services.AddHostedService<RentalDeletedEventConsumer>();
builder.Services.AddHostedService<RentalUpdatedEventConsumer>();

builder.Services.AddSingleton<MongoRentalInboxContext>(p =>
{
    return new MongoRentalInboxContext(
    mongoClient: new MongoClient(MongoDbConstants.CONNECTION_STRING),
    databaseName: "InboxDB");
});

builder.Services.AddSingleton<IRentalInboxRepository, RentalInboxMongoRepository>();


builder.Services.AddMongoRentalACarNowDBContext();
builder.Services.AddSingleton<IMongoRentalWriteRepository, MongoRentalWriteRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "RentalProjectionService");

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IDateService,UtcNowDateService>();

var host = builder.Build();


host.Run();
