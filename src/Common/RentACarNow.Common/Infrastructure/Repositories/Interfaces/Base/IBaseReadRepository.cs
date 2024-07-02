using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Models;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using System.Linq.Expressions;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base
{
    public interface IBaseReadRepository<TEntity> where TEntity : MongoBaseEntity
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
