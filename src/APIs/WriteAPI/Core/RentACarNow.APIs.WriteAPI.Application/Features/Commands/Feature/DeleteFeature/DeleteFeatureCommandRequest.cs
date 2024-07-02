using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.DeleteFeature
{
    public class DeleteFeatureCommandRequest : IRequest<DeleteFeatureCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
