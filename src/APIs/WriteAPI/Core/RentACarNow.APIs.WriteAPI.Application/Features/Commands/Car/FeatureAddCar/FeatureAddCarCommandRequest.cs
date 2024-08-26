using MediatR;
namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureAddCar
{
    public class FeatureAddCarCommandRequest : IRequest<FeatureAddCarCommandResponse>
    {
        public Guid CarId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }


}
