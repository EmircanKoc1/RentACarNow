
using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserClaimDeletedEvent : BaseEvent
    {
        public UserClaimDeletedEvent(Guid userId, Guid claimId)
        {
            UserId = userId;
            ClaimId = claimId;
        }

        public Guid UserId { get; init; }

        public Guid ClaimId { get; set; }
    }
}
