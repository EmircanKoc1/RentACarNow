using Microsoft.Extensions.DependencyInjection;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Infrastructure.CustomCacheServices;
using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;
using System.Collections.Concurrent;
using ST = System.Timers;
namespace RentACarNow.APIs.ReadAPI.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddSingleton<ConcurrentDictionary<Guid, User>>();
            services.AddSingleton<ConcurrentDictionary<Guid, Brand>>();
            services.AddSingleton<ConcurrentDictionary<Guid, Claim>>();
            services.AddSingleton<ConcurrentDictionary<Guid, Car>>();
            services.AddSingleton<ConcurrentDictionary<Guid, Rental>>();


            services.AddTransient<ST.Timer>();

            services.AddTransient<ConcurrentDictionary<DateTime, Guid>>();

            services.AddSingleton<IDateService, UtcNowDateService>();


            services.AddSingleton<ICustomBrandCacheService, CustomBrandCacheService>();
            services.AddSingleton<ICustomCarCacheService, CustomCarCacheService>();
            services.AddSingleton<ICustomClaimCacheService, CustomClaimCacheService>();
            services.AddSingleton<ICustomRentalCacheService, CustomRentalCacheService>();
            services.AddSingleton<ICustomUserCacheService, CustomUserCacheService>();



            return services;

        }

    }
}
