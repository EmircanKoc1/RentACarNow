﻿using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations
{
    public class MongoUserOutboxContext : BaseMongoOutboxContext<UserOutboxMessage>, IMongoUserOutboxContext
    {
        public MongoUserOutboxContext(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName)
        {
        }

        protected sealed override string OutboxName => "UserOutbox";
    }
}
