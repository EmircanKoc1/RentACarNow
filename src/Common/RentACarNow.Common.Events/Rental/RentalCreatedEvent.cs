using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Rental
{
    public class RentalCreatedEvent : BaseEvent
    {
        public RentalCreatedEvent(Guid rentalId, 
            DateTime rentalStartedDate, 
            DateTime rentalEndDate, 
            decimal hourlyRentalPrice,
            decimal totalRentalPrice, 
            CarMessage car,
            UserMessage user, 
            RentalStatus status, 
            DateTime createdDate)
        {
            RentalId = rentalId;
            RentalStartedDate = rentalStartedDate;
            RentalEndDate = rentalEndDate;
            HourlyRentalPrice = hourlyRentalPrice;
            TotalRentalPrice = totalRentalPrice;
            Car = car;
            User = user;
            Status = status;
            CreatedDate = createdDate;
        }

        public Guid RentalId { get; set; }
        public DateTime RentalStartedDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }
        public CarMessage Car { get; set; }
        public UserMessage User { get; set; }
        public RentalStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
