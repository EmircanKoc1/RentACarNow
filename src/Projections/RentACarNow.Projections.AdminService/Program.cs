using RabbitMQ.Client;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.AdminService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddSingleton<IRabbitMQMessageService, RabbitMQMessageService>(x =>
            {
                return new RabbitMQMessageService(new ConnectionFactory()
                {
                    Uri = new Uri("amqp:localhost:5672")
                });
            });

            var host = builder.Build();
            host.Run();
        }
    }
}