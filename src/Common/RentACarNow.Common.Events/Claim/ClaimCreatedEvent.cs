using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimCreatedEvent : BaseEvent
    {
        public ClaimCreatedEvent(Guid claimId, string key, string value, DateTime createdDate)
        {
            ClaimId = claimId;
            Key = key;
            Value = value;
            CreatedDate = createdDate;
        }

        public Guid ClaimId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
