using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Rental
{
    public class RentalUpdatedEvent : BaseEvent
    {
        public Guid Id { get; set; }
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }

        public CarMessage Car { get; set; }
        public CustomerMessage Customer { get; set; }
        public RentalStatus Status { get; set; }
    }
}
