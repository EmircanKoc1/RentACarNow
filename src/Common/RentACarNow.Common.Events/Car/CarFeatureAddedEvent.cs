using RentACarNow.Common.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Car
{
    public class CarFeatureAddedEvent : BaseEvent
    {
        public CarFeatureAddedEvent(
            Guid featureId, 
            Guid carId, 
            string name, 
            string value)
        {
            FeatureId = featureId;
            CarId = carId;
            Name = name;
            Value = value;
        }

        public Guid FeatureId { get; set; }
        public Guid CarId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
