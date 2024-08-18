using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base
{
    public abstract class EfCoreBaseWriteRepository<TEntity> : IEfWriteRepository<TEntity>
        where TEntity : EFBaseEntity, new()
    {
        protected readonly RentalACarNowDbContext _context;
        protected DbSet<TEntity> _table => _context.Set<TEntity>();
        public EfCoreBaseWriteRepository(RentalACarNowDbContext context)
            => _context = context;

        public async Task AddAsync(TEntity entity)
            => await _table.AddAsync(entity);

        public void Delete(TEntity entity)
            => Task.FromResult(_table.Remove(entity));

        public void DeleteById(Guid id)
            => Delete(new TEntity() { Id = id });

        public Task UpdateAsync(TEntity entity)
            => Task.FromResult(_table.Update(entity));

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void SaveChanges()
            => _context.SaveChanges();

        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _context.Database.BeginTransactionAsync();

        public IDbContextTransaction BeginTransaction()
           => _context.Database.BeginTransaction();



    }
}
