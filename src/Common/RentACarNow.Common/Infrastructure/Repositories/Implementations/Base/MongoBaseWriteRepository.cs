

using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Base
{
    public abstract class MongoBaseWriteRepository<TEntity> : IMongoWriteRepository<TEntity>
        where TEntity : BaseEntity, IMongoEntity, new()
    {

        public MongoBaseWriteRepository(MongoRentalACarNowDbContext context)
            => _context = context;

        protected readonly MongoRentalACarNowDbContext _context;

        protected IMongoCollection<TEntity> _collection => _context.GetCollection<TEntity>();


        public async Task AddAsync(TEntity entity)
            => await _collection.InsertOneAsync(entity);

        public async Task DeleteAsync(TEntity entity)
            => await DeleteByIdAsync(entity.Id);

        public async Task DeleteByIdAsync(Guid id)
            => await _collection.DeleteOneAsync(x => x.Id.Equals(id));


        public async Task UpdateAsync(TEntity entity)
            => await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);

    }
}
