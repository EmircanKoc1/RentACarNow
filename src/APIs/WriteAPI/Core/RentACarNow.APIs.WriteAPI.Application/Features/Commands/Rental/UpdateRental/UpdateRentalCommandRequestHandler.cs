using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.UpdateRental
{
    public class UpdateRentalCommandRequestHandler : IRequestHandler<UpdateRentalCommandRequest, UpdateRentalCommandResponse>
    {
        public Task<UpdateRentalCommandResponse> Handle(UpdateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada kiralama güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

}
