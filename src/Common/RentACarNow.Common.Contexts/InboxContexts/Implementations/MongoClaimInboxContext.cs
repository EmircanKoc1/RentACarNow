using MongoDB.Driver;
using RentACarNow.Common.Entities.InboxEntities;

namespace RentACarNow.Common.Contexts.InboxContexts.Implementations
{
    public class MongoClaimInboxContext : BaseMongoInboxContext<ClaimInboxMessage>
    {
        public MongoClaimInboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected override string InboxName => "ClaimInbox";
    }
}
