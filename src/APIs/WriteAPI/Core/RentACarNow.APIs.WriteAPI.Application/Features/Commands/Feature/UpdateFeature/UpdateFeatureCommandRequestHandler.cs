using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.UpdateFeature
{
    public class UpdateFeatureCommandRequestHandler : IRequestHandler<UpdateFeatureCommandRequest, UpdateFeatureCommandResponse>
    {
        public Task<UpdateFeatureCommandResponse> Handle(UpdateFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada özellik güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

}
