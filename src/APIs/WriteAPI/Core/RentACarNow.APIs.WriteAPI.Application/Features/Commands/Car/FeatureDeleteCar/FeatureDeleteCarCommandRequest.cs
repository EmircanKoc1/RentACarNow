using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureDeleteCar
{
    public class FeatureDeleteCarCommandRequest : IRequest<FeatureDeleteCarCommandResponse>
    {
        //public Guid CarId { get; set; }

        public Guid FeatureId { get; set; }
    }
}
