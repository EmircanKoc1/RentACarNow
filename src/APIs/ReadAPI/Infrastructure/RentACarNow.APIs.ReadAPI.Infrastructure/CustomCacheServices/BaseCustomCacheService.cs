using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using ST = System.Timers;

namespace RentACarNow.APIs.ReadAPI.Infrastructure.CustomCacheServices
{
    public abstract class BaseCustomCacheService<TEntity> : ICustomCacheService<TEntity>, IDisposable
        where TEntity : MongoBaseEntity
    {
        protected readonly ConcurrentDictionary<Guid, TEntity> _cacheStorage;
        protected readonly ConcurrentDictionary<DateTime, Guid> _entityCacheTimes;
        protected long dbEntityCount;
        protected readonly ST.Timer _cleanupTimer;
        protected readonly IDateService _dateService;

        protected BaseCustomCacheService(
            ConcurrentDictionary<Guid, TEntity> cacheStorage,
            ConcurrentDictionary<DateTime, Guid> entityCacheTimes,
            ST.Timer cleanupTimer,
            IDateService dateService)
        {
            _cacheStorage = cacheStorage;
            _entityCacheTimes = entityCacheTimes;
            _cleanupTimer = cleanupTimer;
            _dateService = dateService;

            _cleanupTimer.Interval = TimeSpan.FromMilliseconds(100).TotalMilliseconds;

            _cleanupTimer.Elapsed += (sender, e) =>
            {
                _entityCacheTimes
                    .Where(e => e.Key < _dateService.GetDate())
                    .Select(kvp => kvp)
                    .ToList()
                    .ForEach(kvp =>
                    {
                        _cacheStorage.TryRemove(kvp.Value, out TEntity entity);
                        _entityCacheTimes.TryRemove(kvp);

                    });

            };

            _cleanupTimer.Start();
        }

        public long GetCacheEntityCount() => _cacheStorage.Count;
        public long GetDbEntityCount() => dbEntityCount;

        public IEnumerable<TEntity?> GetEntities(PaginationParameter paginationParameter, Expression<Func<TEntity, bool>> filter)
        {
            var entities = _cacheStorage
                .Skip((paginationParameter.PageNumber - 1) * paginationParameter.Size)
                .Take(paginationParameter.Size)
                .Select(kvp => kvp.Value);

            return entities;

        }

        public TEntity? GetEntity(Guid id)
        {
            return _cacheStorage
                .FirstOrDefault(e => e.Key.Equals(id))
                .Value;
        }

        public void SetDbEntityCount(long entityCount) => dbEntityCount = entityCount;

        public void SetEntities(IEnumerable<TEntity> entities, TimeSpan cacheDeletedTime)
        {
            foreach (var entity in entities)
            {
                _cacheStorage.TryAdd(entity.Id, entity);
                _entityCacheTimes.TryAdd(_dateService.GetDate().Add(cacheDeletedTime), entity.Id);
            }


        }

        public void SetEntity(Guid id, TEntity entity, TimeSpan cacheDeletedTime)
        {
            _cacheStorage.TryAdd(id, entity);
            _entityCacheTimes.TryAdd(_dateService.GetDate().Add(cacheDeletedTime), entity.Id);

        }

        public void Dispose()
        {
            _cleanupTimer?.Dispose();
            _cacheStorage.Clear();
            _entityCacheTimes.Clear();
        }
    }
}
