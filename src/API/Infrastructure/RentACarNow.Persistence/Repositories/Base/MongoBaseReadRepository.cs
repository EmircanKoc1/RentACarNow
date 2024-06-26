using MongoDB.Driver;
using RentACarNow.Application.Enums;
using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Application.Models;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Persistence.Contexts.MongoContexts;

namespace RentACarNow.Persistence.Repositories.Base
{
    public abstract class MongoBaseReadRepository<TEntity> : IMongoReadRepository<TEntity>
        where TEntity : BaseEntity, IMongoEntity , new()
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

        public async Task<IEnumerable<TEntity?>?> GetAllAsync(PaginationParameters paginationParameters, bool tracking = false, OrderedDirection direction = OrderedDirection.None)
        {
            CheckTrackingSupported(tracking);


            var findFluent = _collection.Find(FilterDefinition<TEntity>.Empty).SortByDescending();
                





        }

        public Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false)
        {
            CheckTrackingSupported(tracking);




        }

        public Task<TEntity?> GetFirstOrDefaultAsync(Guid id, bool tracking = false)
        {
            CheckTrackingSupported(tracking);

        }

        public Task<TEntity?> GetLastOrDefaultAsync(Guid id, bool tracking = false)
        {
            CheckTrackingSupported(tracking);

        }
    }
}
