using MongoDB.Driver;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.Models;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;
using System.Linq.Expressions;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Base
{
    public abstract class MongoBaseReadRepository<TEntity> : IMongoReadRepository<TEntity>
        where TEntity : MongoBaseEntity, IMongoEntity, new()
    {
        protected readonly MongoRentalACarNowDbContext _context;

        protected MongoBaseReadRepository(MongoRentalACarNowDbContext context)
            => _context = context;


        protected IMongoCollection<TEntity> _collection => _context.GetCollection<TEntity>();

        public Task<long> CountAsync()
        {
            return _collection.EstimatedDocumentCountAsync();
        }

      

        public async Task<IEnumerable<TEntity?>> GetAllAsync(
            PaginationParameter paginationParameter,
            Expression<Func<TEntity, bool>> filter,
            OrderingParameter orderingParameter)
        {
            IEnumerable<TEntity>? entities = default;
            IFindFluent<TEntity, TEntity> findFluent = _collection.Find(filter);

            if (orderingParameter.Sort)
            {

                var sort = orderingParameter.IsAscending switch
                {
                    true => Builders<TEntity>.Sort.Ascending(orderingParameter.SortingField),
                    false => Builders<TEntity>.Sort.Descending(orderingParameter.SortingField)
                };


                findFluent
                    .Sort(sort);
            }

            entities = await findFluent
                      .Skip((paginationParameter.PageNumber - 1) * paginationParameter.Size)
                      .Limit(paginationParameter.Size)
                      .ToListAsync();

            return entities;

        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            var result = await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();

            return result;
        }


        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
           => await (await _collection.FindAsync(predicate)).FirstOrDefaultAsync();




    }
}

