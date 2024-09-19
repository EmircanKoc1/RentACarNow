using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Infrastructure.CustomCacheServices
{
    public class CustomBrandCacheService : BaseCustomCacheService<Brand> , ICustomBrandCacheService
    {
        public CustomBrandCacheService(
            ConcurrentDictionary<Guid, Brand> cacheStorage, 
            ConcurrentDictionary<DateTime, Guid> entityCacheTimes, 
            System.Timers.Timer cleanupTimer, 
            IDateService dateService) : base(cacheStorage, entityCacheTimes, cleanupTimer, dateService)
        {

        }
    }
}
