using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.DeleteFeature
{
    public class DeleteFeatureCommandRequestHandler : IRequestHandler<DeleteFeatureCommandRequest, DeleteFeatureCommandResponse>
    {
        public Task<DeleteFeatureCommandResponse> Handle(DeleteFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada özellik silme işleminin kodunu yazmanız gerekecek
        }
    }

}
