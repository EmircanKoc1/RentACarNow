using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserClaimAddedEvent : BaseEvent
    {
        public UserClaimAddedEvent(Guid userId, ClaimMessage claim)
        {
            UserId = userId;
            Claim = claim;
        }

        public Guid UserId { get; init; }

        public ClaimMessage Claim { get; init; }
    }
}
