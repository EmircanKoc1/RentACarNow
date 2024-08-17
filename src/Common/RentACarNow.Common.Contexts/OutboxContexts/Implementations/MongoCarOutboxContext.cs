using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public class MongoCarOutboxContext : BaseMongoOutboxContext<CarOutboxMessage>, IMongoCarOutboxContext
    {
        public MongoCarOutboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected sealed override string OutboxName => "CarOutbox";
    }
}
