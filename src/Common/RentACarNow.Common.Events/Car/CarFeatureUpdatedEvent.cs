using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Car
{
    public class CarFeatureUpdatedEvent : BaseEvent
    {
        public Guid CarId { get; set; }
        public Guid FeatureId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
