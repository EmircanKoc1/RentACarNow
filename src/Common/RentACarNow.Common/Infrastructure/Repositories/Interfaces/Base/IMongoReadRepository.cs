using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Models;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;
using System.Linq.Expressions;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base
{
    public interface IMongoReadRepository<TEntity> 
        where TEntity : MongoBaseEntity, IMongoEntity
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
