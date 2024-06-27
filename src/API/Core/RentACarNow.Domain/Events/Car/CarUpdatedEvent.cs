using RentACarNow.Domain.Enums;
using RentACarNow.Domain.Events.Common;
using RentACarNow.Domain.Events.Common.Messages;

namespace RentACarNow.Domain.Events.Car
{
    public class CarUpdatedEvent : BaseEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Modal { get; set; }
        public string Title { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public float Kilometer { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int PassengerCapacity { get; set; }
        public int LuggageCapacity { get; set; }
        public float FuelConsumption { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public FuelType CarFuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public BrandMessage Brand { get; set; }
        ICollection<FeatureMessage> Features { get; set; }
    }
}
