using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified
{
    public interface IRentalOutboxRepository : IBaseOutboxRepository<RentalOutboxMessage>
    {

    }

}
