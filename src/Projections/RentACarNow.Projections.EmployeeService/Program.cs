using RabbitMQ.Client;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Constants.MessageBrokers.UriAndUrl;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Projections.EmployeeService.Services;

namespace RentACarNow.Projections.EmployeeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddSingleton<IMongoEmployeeWriteRepository, MongoEmployeeWriteRepository>();

            //builder.Services.AddSingleton<IRabbitMQMessageService, RabbitMQMessageService>(x =>
            //{
            //    return new RabbitMQMessageService(new ConnectionFactory()
            //    {
            //        Uri = new Uri(RabbitMQUrisAndUrls.RABBITMQ_URI)
            //    });
            //});

            //builder.Services.AddSingleton<MongoRentalACarNowDbContext>(x =>
            //{
            //    return new MongoRentalACarNowDbContext(
            //        connectionString: MongoDbConstants.CONNECTION_STRING,
            //        databaseName: MongoDbConstants.DATABASE_NAME);
            //});
            builder.Services.AddIRabbitMQMessageService(clientName: "EmployeeService");

            builder.Services.AddMongoRentalACarNowDBContext();


            builder.Services.AddHostedService<EmployeeAddedBGService>();
            builder.Services.AddHostedService<EmployeeUpdatedBGService>();
            builder.Services.AddHostedService<EmployeeDeletedBGService>();

            var host = builder.Build();
            host.Run();
        }
    }
}