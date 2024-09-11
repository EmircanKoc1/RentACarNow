using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserClaimUpdatedEvent : BaseEvent
    {
        public Guid ClaimId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
