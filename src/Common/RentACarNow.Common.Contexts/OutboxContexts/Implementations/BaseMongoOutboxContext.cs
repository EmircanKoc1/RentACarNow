using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public abstract class BaseMongoOutboxContext<TOutboxMessage> : IBaseMongoOutboxContext<TOutboxMessage>
        where TOutboxMessage : BaseOutboxMessage
    {

        protected abstract string OutboxName { get; }

        private readonly IMongoDatabase _mongoDatabase;
        protected BaseMongoOutboxContext(MongoClient mongoClient, string databaseName)
        {
            IMongoClient client = mongoClient;
            _mongoDatabase = client.GetDatabase(databaseName);
        }

        public IMongoCollection<TOutboxMessage> GetCollection => _mongoDatabase.GetCollection<TOutboxMessage>(GetOutboxName().ToLowerInvariant());

        private string GetOutboxName() => OutboxName ?? "OutboxNameNotDefined";

        public IClientSessionHandle StartSession()
            => _mongoDatabase.Client.StartSession();

        public async Task<IClientSessionHandle> StartSessionAsync()
            => await _mongoDatabase.Client.StartSessionAsync();

    }
}
