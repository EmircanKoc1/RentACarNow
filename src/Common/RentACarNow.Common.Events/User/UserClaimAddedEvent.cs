using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserClaimAddedEvent
    {
        public Guid UserId { get; init; }

        public ClaimMessage Claim { get; init; }
    }
}
