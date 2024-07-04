using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental;
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


            services.AddValidatorsFromAssemblyContaining<CreateRentalCommandRequestValidator>();

            return services;


        }

    }
}
