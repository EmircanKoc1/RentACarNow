using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Enums;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.MongoEntities
{
    public class Rental : BaseEntity, IMongoEntity
    {
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }

        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public RentalStatus Status { get; set; }

    }
}
