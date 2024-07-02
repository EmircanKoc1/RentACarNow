using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Feature
{
    public class FeatureAddedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public CarMessage Car { get; set; }
    }
}
