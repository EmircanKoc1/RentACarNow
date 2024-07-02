using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Car.DeleteCar
{
    public class DeleteCarCommandRequestHandler : IRequestHandler<DeleteCarCommandRequest, DeleteCarCommandResponse>
    {
        public Task<DeleteCarCommandResponse> Handle(DeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada araç silme işleminin kodunu yazmanız gerekecek
        }
    }

}
