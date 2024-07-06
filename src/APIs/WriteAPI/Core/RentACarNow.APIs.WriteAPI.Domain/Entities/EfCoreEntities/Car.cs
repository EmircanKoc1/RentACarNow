using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Common.Enums.EntityEnums;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Car : EFBaseEntity, IEFEntity
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
        public FuelType CarFuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Feature> Features { get; set; } = new HashSet<Feature>();
        public ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();

    }
}
