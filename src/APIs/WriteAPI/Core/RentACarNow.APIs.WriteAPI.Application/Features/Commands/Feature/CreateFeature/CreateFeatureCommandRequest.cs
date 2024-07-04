using MediatR;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.CreateFeature
{
    public class CreateFeatureCommandRequest : IRequest<CreateFeatureCommandResponse>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Guid CarId { get; set; }


    }

}
