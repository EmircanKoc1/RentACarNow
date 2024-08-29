using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public class MongoRentalOutboxContext : BaseMongoOutboxContext<RentalOutboxMessage> ,IMongoRentalOutboxContext
    {
        public MongoRentalOutboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected sealed override string OutboxName => "RentalOutbox";
    }
}
