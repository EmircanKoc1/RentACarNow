using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureUpdateCar
{
    public class FeatureUpdateCarCommandRequest : IRequest<FeatureUpdateCarCommandResponse>
    {
        public Guid FeatureId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
