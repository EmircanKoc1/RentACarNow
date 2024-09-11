using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Car
{
    public class CarUpdatedEvent : BaseEvent
    {
        public CarUpdatedEvent(
            Guid carId, 
            string name, 
            string modal, 
            string title, 
            decimal hourlyRentalPrice, 
            float kilometer,
            string description,
            string color, 
            int passengerCapacity, 
            int luggageCapacity, 
            float fuelConsumption, 
            DateTime? releaseDate, 
            FuelType carFuelType, 
            TransmissionType transmissionType, 
            BrandMessage brand, 
            DateTime updatedDate)
        {
            CarId = carId;
            Name = name;
            Modal = modal;
            Title = title;
            HourlyRentalPrice = hourlyRentalPrice;
            Kilometer = kilometer;
            Description = description;
            Color = color;
            PassengerCapacity = passengerCapacity;
            LuggageCapacity = luggageCapacity;
            FuelConsumption = fuelConsumption;
            ReleaseDate = releaseDate;
            CarFuelType = carFuelType;
            TransmissionType = transmissionType;
            Brand = brand;
            UpdatedDate = updatedDate;
        }

        public Guid CarId { get; set; }
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
        public DateTime UpdatedDate { get; set; }

    }
}
