using MongoDB.Driver;
using RentACarNow.Common.Entities.InboxEntities;

namespace RentACarNow.Common.Contexts.InboxContexts.Implementations
{
    public class MongoBrandInboxContext : BaseMongoInboxContext<BrandInboxMessage>
    {
        public MongoBrandInboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected override string InboxName => "BrandInbox";
    }
}
