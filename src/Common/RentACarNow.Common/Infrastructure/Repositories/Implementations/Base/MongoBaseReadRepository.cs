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


        public async Task<IEnumerable<TEntity?>> GetAllAsync(
        PaginationParameters paginationParameters,
        Expression<Func<TEntity, object>> field,
        Expression<Func<TEntity, bool>> filter,
        OrderedDirection direction = OrderedDirection.None)
        {
            IEnumerable<TEntity> entities = null;

            if (!(direction is OrderedDirection.None))
            {
                var sort = direction switch
                {
                    OrderedDirection.Ascending => Builders<TEntity>.Sort.Ascending(field),
                    OrderedDirection.Descending => Builders<TEntity>.Sort.Descending(field)
                }; ;

                entities = await _collection.Find(filter)
                      .Sort(sort)
                      .Skip((paginationParameters.PageNumber - 1) * paginationParameters.Size)
                      .Limit(paginationParameters.Size)
                      .ToListAsync();

            }

            entities = await _collection.Find(filter)
                  .Skip((paginationParameters.PageNumber - 1) * paginationParameters.Size)
                  .Limit(paginationParameters.Size)
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

