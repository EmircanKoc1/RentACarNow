using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public class MongoBrandOutboxContext : BaseMongoOutboxContext<BrandOutbox>, IMongoBrandOutbox
    {
        public MongoBrandOutboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }
    }
}
