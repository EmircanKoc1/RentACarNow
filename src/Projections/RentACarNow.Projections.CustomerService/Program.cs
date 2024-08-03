using RabbitMQ.Client;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Constants.MessageBrokers.UriAndUrl;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Projections.CustomerService.Services;

namespace RentACarNow.Projections.CustomerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddSingleton<IMongoCustomerWriteRepository, MongoCustomerWriteRepository>();

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

            builder.Services.AddIRabbitMQMessageService(clientName: "CustomerService");

            builder.Services.AddMongoRentalACarNowDBContext();



            builder.Services.AddHostedService<CustomerAddedBGService>();
            builder.Services.AddHostedService<CustomerDeletedBGService>();
            builder.Services.AddHostedService<CustomerUpdatedBGService>();

            var host = builder.Build();
            host.Run();
        }
    }
}