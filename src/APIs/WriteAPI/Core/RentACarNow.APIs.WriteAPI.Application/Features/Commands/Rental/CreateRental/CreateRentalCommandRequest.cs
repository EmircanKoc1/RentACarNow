using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequest : IRequest<CreateRentalCommandResponse>
    {
        public Guid Id{ get; set; }
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public decimal TotalRentalPrice { get; set; }

        public Guid CustomerId{ get; set; }
        public Guid CarId{ get; set; }



    }

}
