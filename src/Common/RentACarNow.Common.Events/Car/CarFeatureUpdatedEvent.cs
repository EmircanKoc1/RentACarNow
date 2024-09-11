using RentACarNow.Common.Events.Common;
using System.Runtime;

namespace RentACarNow.Common.Events.Car
{
    public class CarFeatureUpdatedEvent : BaseEvent
    {
        public CarFeatureUpdatedEvent(
            Guid carId, 
            Guid featureId, 
            string name, 
            string value)
        {
            CarId = carId;
            FeatureId = featureId;
            Name = name;
            Value = value;
        }

        public Guid CarId { get; set; }
        public Guid FeatureId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }


    }
}
