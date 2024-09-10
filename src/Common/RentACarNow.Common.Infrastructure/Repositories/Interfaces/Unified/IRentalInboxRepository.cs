using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified
{
    public interface IRentalInboxRepository : IBaseInboxRepository<RentalInboxMessage>
    {
    }
}
