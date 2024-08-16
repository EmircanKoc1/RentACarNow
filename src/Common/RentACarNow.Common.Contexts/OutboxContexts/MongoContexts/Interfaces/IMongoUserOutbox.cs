using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces
{
    public interface IMongoUserOutbox : IBaseMongoOutboxContext<UserOutbox>
    {
    }
}
