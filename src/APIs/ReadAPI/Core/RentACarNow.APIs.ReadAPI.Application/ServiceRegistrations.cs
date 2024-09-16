using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById;
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

            //services.AddSingleton<MongoRentalACarNowDbContext>(x =>
            //{
            //    return new MongoRentalACarNowDbContext(
            //        connectionString: MongoDbConstants.CONNECTION_STRING,
            //        databaseName: MongoDbConstants.DATABASE_NAME);
            //});

            services.AddMongoRentalACarNowDBContext();

            services.AddTransient<ResponseWrapper<GetByIdBrandQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdBrandQueryResponse>>();


            services.AddTransient<ResponseWrapper<GetByIdCarQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdCarQueryResponse>>();

            services.AddTransient<ResponseWrapper<GetByIdClaimQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdClaimQueryResponse>>();

            services.AddTransient<ResponseWrapper<GetByIdRentalQueryResponse>>();
            services.AddScoped<ResponseBuilder<GetByIdRentalQueryResponse>>();




            return services;

          

        }
    }
}
