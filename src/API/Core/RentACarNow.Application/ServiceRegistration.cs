using Microsoft.Extensions.DependencyInjection;

namespace RentACarNow.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {

                config.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
            });

            return services;

        }


    }
}
