using Microsoft.EntityFrameworkCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Models;
using System.Linq.Expressions;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base
{
    public abstract class EfCoreBaseReadRepository<TEntity> : IEfReadRepository<TEntity>
        where TEntity : EFBaseEntity, IEFBaseEntity
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


        public async Task<IEnumerable<TEntity?>?> GetAllAsync(
            PaginationParameter paginationParameters,
            bool tracking = false,
            Expression<Func<TEntity, object>> keySelector = null,
            OrderedDirection direction = OrderedDirection.None)
        {
            var query = _table.AsQueryable();

            if (!tracking) query.AsNoTracking();

            if (keySelector is null)
            {
                return await query
                    .Skip((paginationParameters.PageNumber - 1) * paginationParameters.Size)
                    .Take(paginationParameters.Size).ToListAsync();
            }

            if (direction is OrderedDirection.Ascending) query.OrderBy(keySelector);
            else if (direction is OrderedDirection.Descending) query.OrderByDescending(keySelector);

            return await query
                    .Skip((paginationParameters.PageNumber - 1) * paginationParameters.Size)
                    .Take(paginationParameters.Size)
                    .ToListAsync();

        }


        public async Task<TEntity?> GetFirstOrDefaultAsync(bool tracking = false, Expression<Func<TEntity, bool>> predicate = null)
        {

            if (!tracking) return await _table.AsNoTracking().FirstOrDefaultAsync(predicate);

            return await _table.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetLastOrDefaultAsync(bool tracking = false, Expression<Func<TEntity, bool>>? predicate = null)
        {

            if (!tracking) return await _table.AsNoTracking().LastOrDefaultAsync(predicate);



            return await _table.LastOrDefaultAsync(predicate);
        }

        public Task<bool> IsExistsAsync(Guid id)
            => _table.AnyAsync(x => x.Id == id);
    }
}
