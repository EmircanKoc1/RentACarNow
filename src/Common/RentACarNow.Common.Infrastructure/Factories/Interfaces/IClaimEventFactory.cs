using RentACarNow.Common.Events.Claim;

namespace RentACarNow.Common.Infrastructure.Factories.Interfaces
{
    public interface IClaimEventFactory
    {

        ClaimCreatedEvent CreateClaimCreatedEvent(Guid claimId, string key, string value, DateTime createdDate);
        ClaimUpdatedEvent CreateClaimUpdatedEvent(Guid claimId, string key, string value, DateTime updatedDate);

        ClaimDeletedEvent CreateClaimDeletedEvent(Guid claimId, DateTime deletedDate);
    }
}
