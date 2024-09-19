using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;
using System.Collections.Concurrent;

namespace RentACarNow.APIs.ReadAPI.Infrastructure.CustomCacheServices
{
    public class CustomCarCacheService : BaseCustomCacheService<Car>, ICustomCarCacheService
    {
        public CustomCarCacheService(
            ConcurrentDictionary<Guid, Car> cacheStorage,
            ConcurrentDictionary<DateTime, Guid> entityCacheTimes,
            System.Timers.Timer cleanupTimer,
            IDateService dateService) : base(cacheStorage, entityCacheTimes, cleanupTimer, dateService)
        {
        }
    }
}
