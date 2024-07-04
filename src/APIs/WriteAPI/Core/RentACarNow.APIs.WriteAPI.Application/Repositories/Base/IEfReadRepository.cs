using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Models;
using System.Linq.Expressions;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Base
{
    public interface IEfReadRepository<TEntity>
        where TEntity : EFBaseEntity, IEFBaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false);
        Task<bool> IsExistsAsync(Guid id);
        Task<TEntity?> GetFirstOrDefaultAsync(
             bool tracking = false,
             Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity?> GetLastOrDefaultAsync(
            bool tracking = false,
            Expression<Func<TEntity, bool>> predicate = null);
        Task<IEnumerable<TEntity?>?> GetAllAsync(
            PaginationParameters paginationParameters,
            bool tracking = false,
            Expression<Func<TEntity, object>> keySelector = null,
            OrderedDirection direction = OrderedDirection.None);


    }
}
