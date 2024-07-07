using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.DeleteFeature
{
    public class DeleteFeatureCommandRequest : IRequest<DeleteFeatureCommandResponse>
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }

    }

}
