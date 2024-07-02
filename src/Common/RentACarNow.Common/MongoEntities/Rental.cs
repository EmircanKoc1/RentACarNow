using RentACarNow.Common.Enums;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.MongoEntities
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
