using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Extensions;
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

            services.AddSingleton<IMongoAdminReadRepository, MongoAdminReadRepository>();
            services.AddSingleton<IMongoBrandReadRepository, MongoBrandReadRepository>();
            services.AddSingleton<IMongoCustomerReadRepository, MongoCustomerReadRepository>();
            services.AddSingleton<IMongoCarReadRepository, MongoCarReadRepository>();
            services.AddSingleton<IMongoFeatureReadRepository, MongoFeatureReadRepository>();
            services.AddSingleton<IMongoClaimReadRepository, MongoClaimReadRepository>();
            services.AddSingleton<IMongoEmployeeReadRepository, MongoEmployeeReadRepository>();
            services.AddSingleton<IMongoRentalReadRepository, MongoRentalReadRepository>();

            //services.AddSingleton<MongoRentalACarNowDbContext>(x =>
            //{
            //    return new MongoRentalACarNowDbContext(
            //        connectionString: MongoDbConstants.CONNECTION_STRING,
            //        databaseName: MongoDbConstants.DATABASE_NAME);
            //});

            services.AddMongoRentalACarNowDBContext();

            return services;

          

        }
    }
}
