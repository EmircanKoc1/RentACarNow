using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;

namespace RentACarNow.Common.Infrastructure.Factories.Implementations
{
    public class ClaimEventFactory : IClaimEventFactory
    {
        public ClaimCreatedEvent CreateClaimCreatedEvent(Guid claimId, string key, string value, DateTime createdDate)
        {
            return new ClaimCreatedEvent(claimId, key, value, createdDate);
        }

        public ClaimDeletedEvent CreateClaimDeletedEvent(Guid claimId, DateTime deletedDate)
        {
            return new ClaimDeletedEvent(claimId, deletedDate);
        }

        public ClaimUpdatedEvent CreateClaimUpdatedEvent(Guid claimId, string key, string value, DateTime updatedDate)
        {
            return new ClaimUpdatedEvent(claimId, key, value, updatedDate);

        }
    }
}
