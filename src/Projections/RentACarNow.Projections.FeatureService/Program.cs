using RabbitMQ.Client;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Constants.MessageBrokers.UriAndUrl;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Projections.FeatureService.Services;

namespace RentACarNow.Projections.FeatureService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);


            builder.Services.AddSingleton<IMongoFeatureWriteRepository, MongoFeatureWriteRepository>();

            builder.Services.AddSingleton<IRabbitMQMessageService, RabbitMQMessageService>(x =>
            {
                return new RabbitMQMessageService(new ConnectionFactory()
                {
                    Uri = new Uri(RabbitMQUrisAndUrls.RABBITMQ_URI)
                });
            });

            builder.Services.AddSingleton<MongoRentalACarNowDbContext>(x =>
            {
                return new MongoRentalACarNowDbContext(
                    connectionString: MongoDbConstants.CONNECTION_STRING,
                    databaseName: MongoDbConstants.DATABASE_NAME);
            });


            builder.Services.AddHostedService<FeatureAddedBGService>();
            builder.Services.AddHostedService<FeatureDeletedBGService>();
            builder.Services.AddHostedService<FeatureUpdatedBGService>();

            var host = builder.Build();
            host.Run();
        }
    }
}