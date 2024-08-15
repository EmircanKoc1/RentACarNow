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
        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<IEnumerable<TEntity?>> GetAllAsync(
            PaginationParameter paginationParameter,
            Expression<Func<TEntity, object>> field,
            Expression<Func<TEntity, bool>> filter,
            OrderedDirection direction = OrderedDirection.None);

        public Task<IEnumerable<TEntity?>> GetAllAsync(
            PaginationParameter paginationParameter,
            Expression<Func<TEntity, bool>> filter,
            OrderingParameter orderingParameter);

    }
}
