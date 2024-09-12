using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental;
using RentACarNow.Common.Infrastructure.Factories.Implementations;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
namespace RentACarNow.APIs.WriteAPI.Application
{
    public static class ServiceRegistrations
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = typeof(ServiceRegistrations).Assembly;

            services.AddValidatorsFromAssemblyContaining<CreateRentalCommandRequestValidator>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });

            services.AddAutoMapper(assembly);

            services.AddSingleton<IBrandEventFactory, BrandEventFactory>();
            services.AddSingleton<ICarEventFactory, CarEventFactory>();
            services.AddSingleton<IRentalEventFactory, RentalEventFactory>();
            services.AddSingleton<IUserEventFactory, UserEventFactory>();
            services.AddSingleton<IClaimEventFactory, ClaimEventFactory>();


            services.AddSingleton<IDateService, UtcNowDateService>();
            services.AddSingleton<IGuidService, GuidService>();

            return services;


        }

    }
}
