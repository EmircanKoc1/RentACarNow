using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base
{
    public interface IBaseOutboxMongoRepository<TOutboxMessage> : IBaseOutboxRepository<TOutboxMessage>
        where TOutboxMessage : BaseOutboxMessage
    {
        //IBaseMongoOutboxContext<TOutboxMessage> Context { get; }
    }
}
