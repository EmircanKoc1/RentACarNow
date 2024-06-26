using Microsoft.EntityFrameworkCore;
using RentACarNow.Application.Enums;
using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Application.Models;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using System.Linq.Expressions;

namespace RentACarNow.Persistence.Repositories.Base
{
    public abstract class EfCoreBaseReadRepository<TEntity> : IEfReadRepository<TEntity>
        where TEntity : BaseEntity, IEfEntity
    {

        protected readonly RentalACarNowDbContext _context;
        protected DbSet<TEntity> _table => _context.Set<TEntity>();
        protected EfCoreBaseReadRepository(RentalACarNowDbContext context)
            => _context = context;

        public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false)
        {
            var entity = await _table.FindAsync(id);

            if (entity is null) return null;

            if (!tracking) _context.Entry<TEntity>(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Guid id, bool tracking = false)
        {
            Expression<Func<TEntity, bool>> func = (x) => x.Id.Equals(id);

            if (!tracking) return await _table.AsNoTracking().FirstOrDefaultAsync(func);

            return await _table.FirstOrDefaultAsync(func);
        }

        public async Task<TEntity?> GetLastOrDefaultAsync(Guid id, bool tracking = false)
        {
            Expression<Func<TEntity, bool>> func = (x) => x.Id.Equals(id);

            if (!tracking) return await _table.AsNoTracking().LastOrDefaultAsync(func);

            return await _table.LastOrDefaultAsync(func);
        }

        public async Task<IEnumerable<TEntity?>?> GetAllAsync(
            PaginationParameters paginationParameters,
            bool tracking = false,
            Expression<Func<TEntity, DateTime>> dateSelector = null,
            OrderedDirection direction = OrderedDirection.None)
        {
            var query = _table.AsQueryable();

            if (!tracking) query.AsNoTracking();
            
            if (dateSelector is null)
            {
                return await query
                    .Skip((paginationParameters.PageNumber - 1) * paginationParameters.Size)
                    .Take(paginationParameters.Size).ToListAsync();
            }

            if (direction is OrderedDirection.Ascending) query.OrderBy(dateSelector);
            else query.OrderByDescending(dateSelector);

            return await query
                    .Skip((paginationParameters.PageNumber - 1) * paginationParameters.Size)
                    .Take(paginationParameters.Size)
                    .ToListAsync();

        }

        public Task<IEnumerable<TEntity?>?> GetAllAsync(PaginationParameters paginationParameters, bool tracking = false, OrderedDirection direction = OrderedDirection.None)
        {
            throw new NotImplementedException();
        }
    }
}
