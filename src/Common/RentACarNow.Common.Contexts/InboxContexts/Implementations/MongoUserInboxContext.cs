using MongoDB.Driver;
using RentACarNow.Common.Entities.InboxEntities;

namespace RentACarNow.Common.Contexts.InboxContexts.Implementations
{
    public class MongoUserInboxContext : BaseMongoInboxContext<UserInboxMessage>
    {
        public MongoUserInboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected override string InboxName => "UserInbox";
    }
}
