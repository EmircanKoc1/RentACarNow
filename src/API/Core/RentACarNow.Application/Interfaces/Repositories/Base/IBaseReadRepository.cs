using RentACarNow.Application.Enums;
using RentACarNow.Application.Models;
using RentACarNow.Domain.Entities.Common.Concrete;
using System.Linq.Expressions;

namespace RentACarNow.Application.Interfaces.Repositories.Base
{
    public interface IBaseReadRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false);
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
