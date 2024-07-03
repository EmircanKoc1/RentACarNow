﻿namespace RentACarNow.APIs.WriteAPI.WebAPI
{
    public static class ServiceRegistrations
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ServiceRegistrations).Assembly);
            });


            return services;


        }

    }
}
