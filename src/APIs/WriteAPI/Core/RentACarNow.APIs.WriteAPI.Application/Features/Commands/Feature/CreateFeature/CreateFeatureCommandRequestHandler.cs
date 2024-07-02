using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.CreateFeature
{
    public class CreateFeatureCommandRequestHandler : IRequestHandler<CreateFeatureCommandRequest, CreateFeatureCommandResponse>
    {
        public Task<CreateFeatureCommandResponse> Handle(CreateFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada özellik oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

}
