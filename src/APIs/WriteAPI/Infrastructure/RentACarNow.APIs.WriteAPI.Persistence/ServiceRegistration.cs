using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore;

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

            services.AddScoped<IEfCoreClaimReadRepository, EfCoreClaimReadRepository>();
            services.AddScoped<IEfCoreClaimWriteRepository, EfCoreClaimWriteRepository>();


            return services;
        }





    }
}
