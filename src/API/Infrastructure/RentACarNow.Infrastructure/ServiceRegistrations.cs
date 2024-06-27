using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Infrastructure.Services;
using RentACarNow.Persistence.Services;

namespace RentACarNow.Infrastructure
{
    public static class ServiceRegistrations
    {

        public static IServiceCollection ConfigInfrastructureLayer(this IServiceCollection services)
        {

            IIoCRegisterService registerService = new IoCRegisterService();

            //registerService.RegisterServices(services,typeof(IEfReadRepository<>));
            //registerService.RegisterServices(services,typeof(IEfWriteRepository<>));

            //registerService.RegisterServices(services, typeof(IMongoReadRepository<>));
            //registerService.RegisterServices(services, typeof(IMongoWriteRepository<>));



            services.AddSingleton<IRabbitMQMessageService, RabbitMQMessageService>(x =>
            {
                return new RabbitMQMessageService(new RabbitMQ.Client.ConnectionFactory() { Uri = new Uri("amqp:localhost:5672") });
            });







            return services;
        }




    }
}
