using Microsoft.Extensions.DependencyInjection;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;
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





            return services;



        }
    }
}
