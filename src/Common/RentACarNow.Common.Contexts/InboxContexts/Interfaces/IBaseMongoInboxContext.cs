using MongoDB.Driver;
using RentACarNow.Common.Entities.InboxEntities;

namespace RentACarNow.Common.Contexts.InboxContexts.Interfaces
{
    public interface IBaseMongoInboxContext<TInboxMessage>
        where TInboxMessage : IInboxMessage
    {
        IMongoCollection<TInboxMessage> GetCollection { get; }

    }
}
