﻿

using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Base
{
    public abstract class MongoBaseWriteRepository<TEntity> : IMongoWriteRepository<TEntity>
        where TEntity : MongoBaseEntity, IMongoEntity, new()
    {

        public MongoBaseWriteRepository(MongoRentalACarNowDbContext context)
            => _context = context;

        protected readonly MongoRentalACarNowDbContext _context;

        protected IMongoCollection<TEntity> _collection => _context.GetCollection<TEntity>();


        public virtual async Task AddAsync(TEntity entity)
            => await _collection.InsertOneAsync(entity);

        public virtual async Task DeleteAsync(TEntity entity)
            => await DeleteByIdAsync(entity.Id);

        public virtual async Task DeleteByIdAsync(Guid id)
            => await _collection.DeleteOneAsync(x => x.Id.Equals(id));


        public abstract Task UpdateAsync(TEntity entity);


        public async virtual Task UpdateWithRelationDatasAsync(TEntity entity)
        => await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id), entity);

    }
}