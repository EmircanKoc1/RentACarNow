using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.CreateFeature
{
    public class CreateFeatureCommandRequest : IRequest<CreateFeatureCommandResponse>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
