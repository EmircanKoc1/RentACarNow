using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public abstract class BaseMongoOutboxContext<TEntity> : IBaseMongoOutboxContext<TEntity>
        where TEntity : IMongoOutbox
    {
        private readonly IMongoDatabase _mongoDatabase;
        protected BaseMongoOutboxContext(MongoClient mongoClient, string databaseName)
        {
            IMongoClient client = mongoClient;
            _mongoDatabase = client.GetDatabase(databaseName);
        }


        public IMongoCollection<TEntity> GetCollection => _mongoDatabase.GetCollection<TEntity>(nameof(TEntity).ToLowerInvariant());





    }
}
