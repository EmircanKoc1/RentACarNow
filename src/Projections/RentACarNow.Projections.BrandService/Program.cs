
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Projections.BrandService;
using RentACarNow.Projections.BrandService.Consumers;
using RentACarNow.Common.Infrastructure.Extensions;
using System.Reflection;
using RentACarNow.Common.Contexts.InboxContexts.Implementations;
using MongoDB.Driver;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Infrastructure.Services.Implementations;

var builder = Host.CreateApplicationBuilder(args);




builder.Services.AddSingleton<MongoBrandInboxContext>(p =>
{
    return new MongoBrandInboxContext(
    mongoClient: new MongoClient(MongoDbConstants.CONNECTION_STRING),
    databaseName: "InboxDB");
});

builder.Services.AddSingleton<IBrandInboxRepository, BrandInboxMongoRepository>();
builder.Services.AddIRabbitMQMessageService(clientName:"BrandProjectionService");

builder.Services.AddHostedService<BrandCreatedEventConsumer>();
builder.Services.AddHostedService<BrandDeletedEventConsumer>();
builder.Services.AddHostedService<BrandUpdatedEventConsumer>();
builder.Services.AddHostedService<ProjectionService>();

builder.Services.AddMongoRentalACarNowDBContext();
builder.Services.AddSingleton<IMongoBrandWriteRepository,MongoBrandWriteRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<IDateService,UtcNowDateService>();

var host = builder.Build();
host.Run();
