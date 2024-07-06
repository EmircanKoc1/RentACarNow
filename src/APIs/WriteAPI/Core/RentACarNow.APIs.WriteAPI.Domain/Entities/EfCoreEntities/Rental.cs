using RentACarNow.Common.Enums.EntityEnums;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Rental : Common.Concrete.EFBaseEntity, Common.Interfaces.IEFEntity
    {
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public RentalStatus Status { get; set; }

    }
}
