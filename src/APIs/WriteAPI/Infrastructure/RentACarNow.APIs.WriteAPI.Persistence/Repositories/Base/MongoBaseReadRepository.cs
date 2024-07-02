using MongoDB.Driver;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.MongoContexts;
using RentACarNow.Application.Enums;
using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Application.Models;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Models;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using System.Linq.Expressions;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base
{
    public abstract class MongoBaseReadRepository<TEntity> : IMongoReadRepository<TEntity>
        where TEntity : BaseEntity, IMongoEntity, new()
    {
        protected readonly MongoRentalACarNowDbContext _context;

        protected MongoBaseReadRepository(MongoRentalACarNowDbContext context)
        {
            _context = context;
        }

        protected IMongoCollection<TEntity> _collection => _context.GetCollection<TEntity>();

        protected void CheckTrackingSupported(bool tracking)
        {
            if (tracking) throw new NotSupportedException("MongoDB doesn't support tracking");
        }

        public async Task<IEnumerable<TEntity?>?> GetAllAsync(PaginationParameters paginationParameters, bool tracking = false, Expression<Func<TEntity, object>> keySelector = null, OrderedDirection direction = OrderedDirection.None)
        {

            return _collection.Find(FilterDefinition<TEntity>.Empty).ToList();

        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false)
        {
            CheckTrackingSupported(tracking);

            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            var result = await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();

            return result;
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Guid id, bool tracking = false)
        {
            CheckTrackingSupported(tracking);

            return await GetByIdAsync(id, tracking);

        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(bool tracking = false, Expression<Func<TEntity, bool>> predicate = null)
        {
            CheckTrackingSupported(tracking);

            var result = await (await _collection.FindAsync(predicate)).FirstOrDefaultAsync();

            return result;
        }


        public async Task<TEntity?> GetLastOrDefaultAsync(bool tracking = false, Expression<Func<TEntity, bool>> predicate = null)
        {

            CheckTrackingSupported(tracking);

            var unaryEx = (UnaryExpression)predicate.Body;
            var memberEx = (MemberExpression)unaryEx.Operand;
            var name = memberEx.Member.Name;


            var sortDefination = Builders<TEntity>.Sort.Descending(name);

            var result = await _collection.Find(x => true).Sort(sortDefination).FirstOrDefaultAsync();

            return result;
        }
    }
}
