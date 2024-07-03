using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Base
{
    public interface IEfWriteRepository<TEntity> 
        where TEntity : EFBaseEntity, IEFBaseEntity
    {

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(Guid id);
        Task SaveChangesAsync();

    }
}
