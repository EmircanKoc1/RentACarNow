using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Car
{
    public class CarFeatureDeletedEvent : BaseEvent
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
    }
}
