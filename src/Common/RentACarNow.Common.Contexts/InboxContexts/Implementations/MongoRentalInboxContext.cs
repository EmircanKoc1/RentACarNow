using MongoDB.Driver;
using RentACarNow.Common.Entities.InboxEntities;

namespace RentACarNow.Common.Contexts.InboxContexts.Implementations
{
    public class MongoRentalInboxContext : BaseMongoInboxContext<RentalInboxMessage>
    {
        public MongoRentalInboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected override string InboxName => "RentalInbox";
    }
}
