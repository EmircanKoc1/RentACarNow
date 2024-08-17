using MongoDB.Driver;
using RentACarNow.Common.Contexts.InboxContexts.Interfaces;
using RentACarNow.Common.Entities.InboxEntities;

namespace RentACarNow.Common.Contexts.InboxContexts.Implementations
{
    public abstract class BaseMongoInboxContext<TInboxMessage> : IBaseMongoInboxContext<TInboxMessage>
        where TInboxMessage : BaseInboxMessage
    {
        protected abstract string InboxName { get; }

        private readonly IMongoDatabase _mongoDatabase;
        protected BaseMongoInboxContext(MongoClient mongoClient, string databaseName)
        {
            IMongoClient client = mongoClient;
            _mongoDatabase = client.GetDatabase(databaseName);
        }
        public IMongoCollection<TInboxMessage> GetCollection
            => _mongoDatabase.GetCollection<TInboxMessage>(GetOutboxName().ToLowerInvariant());

        private string GetOutboxName() => InboxName ?? "InboxNameNotDefined";
    }
}
