using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.InboxContexts.Implementations;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Implementations;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Projections.UserClaimService.Consumers;
using RentACarNow.Projections.UserService;
using RentACarNow.Projections.UserService.Consumers;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ProjectionService>();

builder.Services.AddHostedService<UserClaimAddedEventConsumer>();
builder.Services.AddHostedService<UserClaimDeletedEventConsumer>();
builder.Services.AddHostedService<UserClaimUpdatedEventConsumer>();
builder.Services.AddHostedService<UserCreatedEventConsumer>();
builder.Services.AddHostedService<UserDeletedEventConsumer>();
builder.Services.AddHostedService<UserUpdatedEventConsumer>();




builder.Services.AddSingleton<MongoUserInboxContext>(p =>
{
    return new MongoUserInboxContext(
    mongoClient: new MongoClient(MongoDbConstants.CONNECTION_STRING),
    databaseName: "InboxDB");
});

builder.Services.AddSingleton<IUserInboxRepository, UserInboxMongoRepository>();


builder.Services.AddMongoRentalACarNowDBContext();
builder.Services.AddSingleton<IMongoUserWriteRepository, MongoUserWriteRepository>();

builder.Services.AddIRabbitMQMessageService(clientName: "UserProjectionService");

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<IUserEventFactory, UserEventFactory>();

builder.Services.AddSingleton<IDateService,UtcNowDateService>();

var host = builder.Build();



host.Run();
