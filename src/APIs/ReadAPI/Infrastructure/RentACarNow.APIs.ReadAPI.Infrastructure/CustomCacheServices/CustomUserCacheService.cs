using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using System.Collections.Concurrent;

namespace RentACarNow.APIs.ReadAPI.Infrastructure.CustomCacheServices
{
    public class CustomUserCacheService : BaseCustomCacheService<User>, ICustomUserCacheService
    {
        public CustomUserCacheService(
            ConcurrentDictionary<Guid, User> cacheStorage,
            ConcurrentDictionary<DateTime, Guid> entityCacheTimes,
            System.Timers.Timer cleanupTimer,
            IDateService dateService) : base(cacheStorage, entityCacheTimes, cleanupTimer, dateService)
        {
        }
    }


}


