﻿using Microsoft.Extensions.DependencyInjection;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetById;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
namespace RentACarNow.APIs.ReadAPI.Application
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

            services.AddSingleton<IMongoBrandReadRepository, MongoBrandReadRepository>();
            services.AddSingleton<IMongoCarReadRepository, MongoCarReadRepository>();
            services.AddSingleton<IMongoFeatureReadRepository, MongoFeatureReadRepository>();
            services.AddSingleton<IMongoClaimReadRepository, MongoClaimReadRepository>();
            services.AddSingleton<IMongoRentalReadRepository, MongoRentalReadRepository>();
            services.AddSingleton<IMongoUserReadRepository, MongoUserReadRepository>();

            //services.AddSingleton<MongoRentalACarNowDbContext>(x =>
            //{
            //    return new MongoRentalACarNowDbContext(
            //        connectionString: MongoDbConstants.CONNECTION_STRING,
            //        databaseName: MongoDbConstants.DATABASE_NAME);
            //});

            services.AddMongoRentalACarNowDBContext();

            #region start brand
            services.AddTransient<ResponseWrapper<GetByIdBrandQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdBrandQueryResponse>>();

            services.AddTransient<ResponseWrapper<IEnumerable<GetAllBrandQueryResponse>>>();
            services.AddScoped<ResponseBuilder<IEnumerable<GetAllBrandQueryResponse>>>();

            #endregion

            #region start car

            services.AddTransient<ResponseWrapper<GetByIdCarQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdCarQueryResponse>>();


            services.AddTransient<ResponseWrapper<IEnumerable<GetAllCarQueryResponse>>>();
            services.AddScoped<ResponseBuilder<IEnumerable<GetAllCarQueryResponse>>>();

            #endregion

            #region start claim

            services.AddTransient<ResponseWrapper<GetByIdClaimQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdClaimQueryResponse>>();


            services.AddTransient<ResponseWrapper<IEnumerable<GetAllClaimQueryResponse>>>();
            services.AddScoped<ResponseBuilder<IEnumerable<GetAllClaimQueryResponse>>>();

            #endregion

            #region start rental

            services.AddTransient<ResponseWrapper<GetByIdRentalQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdRentalQueryResponse>>();


            services.AddTransient<ResponseWrapper<IEnumerable<GetAllRentalQueryResponse>>>();
            services.AddScoped<ResponseBuilder<IEnumerable<GetAllRentalQueryResponse>>>();


            #endregion

            #region start user

            services.AddTransient<ResponseWrapper<GetByIdUserQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdUserQueryResponse>>();


            services.AddTransient<ResponseWrapper<IEnumerable<GetAllUserQueryResponse>>>();
            services.AddScoped<ResponseBuilder<IEnumerable<GetAllUserQueryResponse>>>();


            #endregion



            return services;



        }
    }
}
