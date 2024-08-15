using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Constants.MessageBrokers.UriAndUrl;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoContexts.Implementations;

namespace RentACarNow.Common.Infrastructure.Extensions
{
    public static class IOCRegisterExtensions
    {
        public static IServiceCollection AddIRabbitMQMessageService(
            this IServiceCollection services,
            string rabbitMQUri = RabbitMQUrisAndUrls.RABBITMQ_URI,
            string clientName = "ClientNameNotDefined")
        {

            services.AddSingleton<IRabbitMQMessageService, RabbitMQMessageService>(x =>
            {
                return new RabbitMQMessageService(new ConnectionFactory()
                {
                    Uri = new Uri(rabbitMQUri),
                    ClientProvidedName = clientName
                });
            });


            return services;
        }



        public static IServiceCollection AddMongoRentalACarNowDBContext(
            this IServiceCollection services,
            string connectionString = MongoDbConstants.CONNECTION_STRING,
            string databaseName = MongoDbConstants.DATABASE_NAME)
        {


            services.AddSingleton<MongoRentalACarNowDbContext>(x =>
            {
                return new MongoRentalACarNowDbContext(
                    connectionString: connectionString,
                    databaseName: databaseName);
            });


            return services;

        }

    }
}
