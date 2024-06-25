using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Enums;

namespace RentACarNow.Domain.Entities.MongoEntities
{
    public class Rental : BaseEntity , IEfEntity
    {
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }   
        public Car Car { get; set; }

        public RentalStatus Status { get; set; }

    }
}
