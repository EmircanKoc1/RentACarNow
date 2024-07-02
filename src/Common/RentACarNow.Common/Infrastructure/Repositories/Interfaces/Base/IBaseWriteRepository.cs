using RentACarNow.Common.MongoEntities.Common.Concrete;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base
{
    public interface IBaseWriteRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(Guid id);


    }
}
