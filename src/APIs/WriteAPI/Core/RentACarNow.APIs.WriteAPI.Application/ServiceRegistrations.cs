﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
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

           

            return services;


        }

    }
}
