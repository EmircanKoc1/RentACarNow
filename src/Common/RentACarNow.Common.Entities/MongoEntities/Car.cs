using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.MongoEntities
{
    public class Car : MongoBaseEntity
    {

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
        public bool IsRental { get; set; }
        public FuelType CarFuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Feature> Features { get; set; }

    }
}
