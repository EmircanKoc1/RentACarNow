using Microsoft.Extensions.DependencyInjection;

namespace RentACarNow.APIs.WriteAPI.Application
{
    public static class ServiceRegistrations
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = typeof(ServiceRegistrations).Assembly;

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });

            services.AddAutoMapper(assembly);


            return services;


        }

    }
}
