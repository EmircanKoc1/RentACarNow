using RabbitMQ.Client;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Constants.MessageBrokers.UriAndUrl;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Projections.AdminService.Services;
using RentACarNow.Projections.AdminService.Services.RentACarNow.Projections.AdminService.Services;

namespace RentACarNow.Projections.AdminService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);


            builder.Services.AddSingleton<IMongoAdminWriteRepository, MongoAdminWriteRepository>();

            builder.Services.AddIRabbitMQMessageService(clientName:"AdminService");

            builder.Services.AddMongoRentalACarNowDBContext();

            builder.Services.AddHostedService<AdminAddedBGService>();
            builder.Services.AddHostedService<AdminDeletedBGService>();
            builder.Services.AddHostedService<AdminUpdatedBGService>();

            var host = builder.Build();
            host.Run();
        }
    }
}