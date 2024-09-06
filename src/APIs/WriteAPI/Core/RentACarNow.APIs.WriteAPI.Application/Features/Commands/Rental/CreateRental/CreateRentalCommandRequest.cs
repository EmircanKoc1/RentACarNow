using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequest : IRequest<CreateRentalCommandResponse>
    {
        public DateTime? RentalStartedDate { get; set; }
        public DateTime? RentalEndDate { get; set; }

        public Guid UserId { get; set; }
        public Guid CarId { get; set; }



    }

}
