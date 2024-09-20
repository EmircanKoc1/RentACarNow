using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Interfaces.Services
{
    public interface ICustomCacheService<TEntity>
    {
        TEntity? GetEntity(Guid id);

        void SetEntity(Guid id, TEntity entity, TimeSpan cacheDeleteTime);
        void SetDbEntityCount(long entityCount);
        long GetDbEntityCount();
        long GetCacheEntityCount();
        void SetEntities(IEnumerable<TEntity> entities, TimeSpan cacheDeleteTime);

        IEnumerable<TEntity?> GetEntities(
            PaginationParameter paginationParameter,
            Func<KeyValuePair<Guid, TEntity>, bool> filter);


    }

}
