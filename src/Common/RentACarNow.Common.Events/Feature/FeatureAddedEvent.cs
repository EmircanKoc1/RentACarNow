using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Feature
{
    public class FeatureAddedCarEvent : BaseEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid CarId { get; set; }
        //public CarMessage Car { get; set; }
    }
}
