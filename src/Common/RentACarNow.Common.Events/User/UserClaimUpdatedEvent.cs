using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserClaimUpdatedEvent : BaseEvent
    {
        public UserClaimUpdatedEvent(Guid claimId, string key, string value)
        {
            ClaimId = claimId;
            Key = key;
            Value = value;
        }

        public Guid ClaimId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
