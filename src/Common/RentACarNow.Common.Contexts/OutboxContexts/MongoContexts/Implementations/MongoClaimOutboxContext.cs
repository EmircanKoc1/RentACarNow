using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public class MongoClaimOutboxContext : BaseMongoOutboxContext<ClaimOutbox>, IMongoClaimOutbox
    {
        public MongoClaimOutboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }
    }
}
