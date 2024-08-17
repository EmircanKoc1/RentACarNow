using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public class MongoBrandOutboxContext : BaseMongoOutboxContext<BrandOutboxMessage>, IMongoBrandOutboxContext
    {
        public MongoBrandOutboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected sealed override string OutboxName => "BrandOutbox";
    }
}
