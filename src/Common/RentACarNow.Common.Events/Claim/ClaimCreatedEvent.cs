using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimCreatedEvent : BaseEvent
    {
        public ClaimCreatedEvent(Guid claimId, string key, string value, DateTime claimCreatedDate)
        {
            ClaimId = claimId;
            Key = key;
            Value = value;
            ClaimCreatedDate = claimCreatedDate;
        }

        public Guid ClaimId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public DateTime ClaimCreatedDate { get; set; }

    }
}
