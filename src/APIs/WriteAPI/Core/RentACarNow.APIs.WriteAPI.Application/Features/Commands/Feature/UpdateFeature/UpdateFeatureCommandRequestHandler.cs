using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.UpdateFeature
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
