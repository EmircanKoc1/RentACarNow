using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Rental
{
    public class RentalUpdatedEvent : BaseEvent
    {
        public RentalUpdatedEvent(Guid rentalId, DateTime? rentalStartedDate, DateTime? rentalEndDate, decimal hourlyRentalPrice, decimal totalRentalPrice, CarMessage car, UserMessage user, RentalStatus status, DateTime updatedDate)
        {
            RentalId = rentalId;
            RentalStartedDate = rentalStartedDate;
            RentalEndDate = rentalEndDate;
            HourlyRentalPrice = hourlyRentalPrice;
            TotalRentalPrice = totalRentalPrice;
            Car = car;
            User = user;
            Status = status;
            UpdatedDate = updatedDate;
        }

        public Guid RentalId { get; set; }
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }
        public CarMessage Car { get; set; }
        public UserMessage User { get; set; }
        public RentalStatus Status { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
