using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.MongoEntities.Common.Concrete;

namespace RentACarNow.Common.MongoEntities
{
    public class Rental : MongoBaseEntity
    {
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }

        public Car Car { get; set; }
        public User user { get; set; }
        public RentalStatus Status { get; set; }

    }
}
