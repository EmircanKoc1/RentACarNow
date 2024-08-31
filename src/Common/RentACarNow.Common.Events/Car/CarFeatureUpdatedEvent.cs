using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Car
{
    public class CarFeatureUpdatedEvent : BaseEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid CarId { get; set; }

    }
}
