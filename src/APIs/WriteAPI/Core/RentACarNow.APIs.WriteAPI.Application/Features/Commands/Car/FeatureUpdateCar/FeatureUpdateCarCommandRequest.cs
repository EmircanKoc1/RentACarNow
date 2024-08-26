using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureUpdateCar
{
    public class FeatureUpdateCarCommandRequest : IRequest<FeatureUpdateCarCommandResponse>
    {
        public Guid CarId { get; set; }
        public int FeatureId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
