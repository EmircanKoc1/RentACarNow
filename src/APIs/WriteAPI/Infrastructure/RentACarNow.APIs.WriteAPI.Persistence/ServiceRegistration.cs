using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.UnitOfWorks;
using RentACarNow.Common.Constants.Databases;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.MongoContexts.Implementations;

namespace RentACarNow.APIs.WriteAPI.Persistence
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RentalACarNowDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            services.AddScoped<IEfCoreBrandReadRepository, EfCoreBrandReadRepository>();
            services.AddScoped<IEfCoreBrandWriteRepository, EfCoreBrandWriteRepository>();

            services.AddScoped<IEfCoreCarReadRepository, EfCoreCarReadRepository>();
            services.AddScoped<IEfCoreCarWriteRepository, EfCoreCarWriteRepository>();

            services.AddScoped<IEfCoreFeatureReadRepository, EfCoreFeatureReadRepository>();
            services.AddScoped<IEfCoreFeatureWriteRepository, EfCoreFeatureWriteRepository>();

            services.AddScoped<IEfCoreRentalReadRepository, EfCoreRentalReadRepository>();
            services.AddScoped<IEfCoreRentalWriteRepository, EfCoreRentalWriteRepository>();

            services.AddScoped<IEfCoreUserReadRepository, EfCoreUserReadRepository>();
            services.AddScoped<IEfCoreUserWriteRepository, EfCoreUserWriteRepository>();

            services.AddScoped<IEfCoreClaimReadRepository, EfCoreClaimReadRepository>();
            services.AddScoped<IEfCoreClaimWriteRepository, EfCoreClaimWriteRepository>();

            services.AddSingleton<MongoRentalACarNowDbContext>(x =>
            {
                return new MongoRentalACarNowDbContext(
                    connectionString: MongoDbConstants.CONNECTION_STRING,
                    databaseName: MongoDbConstants.DATABASE_NAME);
            });


            services.AddSingleton<MongoBrandOutboxContext>(x =>
            {
                return new MongoBrandOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
            });

            services.AddSingleton<MongoCarOutboxContext>(x =>
            {
                return new MongoCarOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
            });

            services.AddSingleton<MongoClaimOutboxContext>(x =>
            {
                return new MongoClaimOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
            });

            services.AddSingleton<MongoUserOutboxContext>(x =>
            {
                return new MongoUserOutboxContext(new MongoClient(MongoDbConstants.CONNECTION_STRING), "OutboxDB");
            });



            services.AddSingleton<IBrandOutboxRepository, BrandOutboxMongoRepository>();
            services.AddSingleton<IUserOutboxRepository, UserOutboxMongoRepository>();
            services.AddSingleton<IClaimOutboxRepository, ClaimOutboxMongoRepository>();
            services.AddSingleton<ICarOutboxRepository, CarOutboxMongoRepository>();

            services.AddScoped<IRentalUnitOfWork, RentalUnitOfWork>();


            return services;
        }





    }
}
