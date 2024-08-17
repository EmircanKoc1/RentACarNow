using MongoDB.Driver;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces
{
    public interface IBaseMongoOutboxContext<TOutboxMessage>
        where TOutboxMessage : IOutboxMessage
    {
        IMongoCollection<TOutboxMessage> GetCollection { get; } 

    }
}
