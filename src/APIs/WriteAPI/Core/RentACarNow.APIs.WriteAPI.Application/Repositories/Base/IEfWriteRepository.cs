using Microsoft.EntityFrameworkCore.Storage;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Base
{
    public interface IEfWriteRepository<TEntity>
        where TEntity : EFBaseEntity, IEFBaseEntity
    {

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        void DeleteById(Guid id);
        Task SaveChangesAsync();
        void SaveChanges();

        Task<IDbContextTransaction> BeginTransactionAsync();
        IDbContextTransaction BeginTransaction();
    }
}
