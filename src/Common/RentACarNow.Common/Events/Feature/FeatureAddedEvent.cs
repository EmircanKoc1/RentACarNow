using RentACarNow.Domain.Events.Common;
using RentACarNow.Domain.Events.Common.Messages;

namespace RentACarNow.Domain.Events.Feature
{
    public class FeatureAddedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public CarMessage Car { get; set; }
    }
}
