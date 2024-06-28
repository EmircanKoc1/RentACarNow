using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.DeleteRental
{

    public class DeleteRentalCommandRequestHandler : IRequestHandler<DeleteRentalCommandRequest, DeleteRentalCommandResponse>
    {
        public Task<DeleteRentalCommandResponse> Handle(DeleteRentalCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada kiralama silme işleminin kodunu yazmanız gerekecek
        }
    }

}
