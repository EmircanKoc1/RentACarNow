using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequest : IRequest<DeleteRentalCommandResponse>
    {
        public Guid RentalId { get; set; }

    }

}
