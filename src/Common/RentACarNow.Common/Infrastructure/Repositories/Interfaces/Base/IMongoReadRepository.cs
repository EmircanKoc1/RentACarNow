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

        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);
        //Task<TEntity?> GetLastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<IEnumerable<TEntity?>> GetAllAsync(
            PaginationParameters paginationParameters,
            Expression<Func<TEntity, object>> keySelector = null,
            OrderedDirection direction = OrderedDirection.None);


    }
}
