using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimAddedEvent : BaseEvent, IEvent
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
