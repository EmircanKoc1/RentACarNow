using MongoDB.Driver;
using RentACarNow.Common.Entities.InboxEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Contexts.InboxContexts.Implementations
{
    public class MongoCarInboxContext : BaseMongoInboxContext<CarInboxMessage>
    {
        public MongoCarInboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected override string InboxName => "CarInbox";
    }

}
