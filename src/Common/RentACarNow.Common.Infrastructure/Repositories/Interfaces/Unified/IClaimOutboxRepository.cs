using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified
{
    public interface IClaimOutboxRepository : IBaseOutboxRepository<ClaimOutboxMessage>
    {

    }

}
