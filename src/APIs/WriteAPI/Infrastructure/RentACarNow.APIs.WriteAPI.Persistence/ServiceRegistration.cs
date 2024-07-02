using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.MongoContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.Mongo;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.Mongo;
using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;
using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;

namespace RentACarNow.APIs.WriteAPI.Persistence
{
    public static class ServiceRegistration
    {

        public static IServiceCollection ConfigPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RentalACarNowDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });


            services.AddSingleton(x => new MongoRentalACarNowDbContext(configuration));



            services.AddSingleton<IMongoAdminWriteRepository, MongoAdminWriteRepository>();
            services.AddSingleton<IMongoAdminReadRepository, MongoAdminReadRepository>();

            services.AddSingleton<IMongoRentalReadRepository, MongoRentalReadRepository>();
            services.AddSingleton<IMongoRentalWriteRepository, MongoRentalWriteRepository>();

            services.AddSingleton<IMongoCarReadRepository, MongoCarReadRepository>();
            services.AddSingleton<IMongoCarWriteRepository, MongoCarWriteRepository>();

            services.AddSingleton<IMongoBrandReadRepository, MongoBrandReadRepository>();
            services.AddSingleton<IMongoBrandWriteRepository, MongoBrandWriteRepository>();

            services.AddSingleton<IMongoCustomerReadRepository, MongoCustomerReadRepository>();
            services.AddSingleton<IMongoCustomerWriteRepository, MongoCustomerWriteRepository>();

            services.AddScoped<IEfCoreAdminReadRepository, EfCoreAdminReadRepository>();
            services.AddScoped<IEfCoreAdminWriteRepository, EfCoreAdminWriteRepository>();

            services.AddScoped<IEfCoreBrandReadRepository, EfCoreBrandReadRepository>();
            services.AddScoped<IEfCoreBrandWriteRepository, EfCoreBrandWriteRepository>();

            services.AddScoped<IEfCoreCarReadRepository, EfCoreCarReadRepository>();
            services.AddScoped<IEfCoreCarWriteRepository, EfCoreCarWriteRepository>();

            services.AddScoped<IEfCoreCustomerReadRepository, EfCoreCustomerReadRepository>();
            services.AddScoped<IEfCoreCustomerWriteRepository, EfCoreCustomerWriteRepository>();

            services.AddScoped<IEfCoreEmployeeReadRepository, EfCoreEmployeeReadRepository>();
            services.AddScoped<IEfCoreEmployeeWriteRepository, EfCoreEmployeeWriteRepository>();

            services.AddScoped<IEfCoreFeatureReadRepository, EfCoreFeatureReadRepository>();
            services.AddScoped<IEfCoreFeatureWriteRepository, EfCoreFeatureWriteRepository>();

            services.AddScoped<IEfCoreRentalReadRepository, EfCoreRentalReadRepository>();
            services.AddScoped<IEfCoreRentalWriteRepository, EfCoreRentalWriteRepository>();


            return services;
        }





    }
}
