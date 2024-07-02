using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequest : IRequest<DeleteRentalCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
