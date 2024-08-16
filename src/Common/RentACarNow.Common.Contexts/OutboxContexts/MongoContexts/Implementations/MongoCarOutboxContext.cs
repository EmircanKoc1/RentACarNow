using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public class MongoCarOutboxContext : BaseMongoOutboxContext<CarOutbox>, IMongoCarOutbox
    {
        public MongoCarOutboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }
    }
}
