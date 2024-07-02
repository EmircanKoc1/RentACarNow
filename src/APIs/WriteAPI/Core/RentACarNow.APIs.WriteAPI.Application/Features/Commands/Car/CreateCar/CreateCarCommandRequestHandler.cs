using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Car.CreateCar
{
    public class CreateCarCommandRequestHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        public Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada araç oluşturma iş

        }
    }
}
