using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserClaimDeletedEvent : BaseEvent
    {
        public Guid UserId { get; init; }
        public ClaimMessage Claim { get; init; }

    }

    public sealed class UserClaimUpdatedEvent : BaseEvent
    {
        public Guid ClaimId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
