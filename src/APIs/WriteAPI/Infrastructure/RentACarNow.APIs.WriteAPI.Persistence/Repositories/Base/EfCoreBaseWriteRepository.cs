using Microsoft.EntityFrameworkCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base
{
    public abstract class EfCoreBaseWriteRepository<TEntity> : IEfWriteRepository<TEntity>
        where TEntity : BaseEntity, IEFEntity, new()
    {
        protected readonly RentalACarNowDbContext _context;
        protected DbSet<TEntity> _table => _context.Set<TEntity>();
        public EfCoreBaseWriteRepository(RentalACarNowDbContext context)
            => _context = context;

        public async Task AddAsync(TEntity entity)
            => await _table.AddAsync(entity);

        public Task DeleteAsync(TEntity entity)
            => Task.FromResult(_table.Remove(entity));

        public Task DeleteByIdAsync(Guid id)
            => DeleteAsync(new TEntity() { Id = id });

        public Task UpdateAsync(TEntity entity)
            => Task.FromResult(_table.Update(entity));

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();


    }
}
