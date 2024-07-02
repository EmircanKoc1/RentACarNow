using Microsoft.EntityFrameworkCore;
using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Persistence.Contexts.EfCoreContext;

namespace RentACarNow.Persistence.Repositories.Base
{
    public abstract class EfCoreBaseWriteRepository<TEntity> : IEfWriteRepository<TEntity>
        where TEntity : BaseEntity, IEfEntity, new()
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
