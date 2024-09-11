using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Car
{
    public class CarFeatureDeletedEvent : BaseEvent
    {
        public CarFeatureDeletedEvent(
            Guid featureId, 
            Guid carId)
        {
            FeatureId = featureId;
            CarId = carId;
        }

        public Guid FeatureId { get; set; }
        public Guid CarId { get; set; }


    }
}
