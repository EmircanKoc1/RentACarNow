using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserClaimDeletedEvent : BaseEvent
    {
        public Guid UserId { get; init; }
        public ClaimMessage Claim { get; init; }

    }
}
