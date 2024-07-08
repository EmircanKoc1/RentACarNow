using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetById
{
    public class GetByIdFeatureQueryRequestHandler : IRequestHandler<GetByIdFeatureQueryRequest, IEnumerable<GetByIdFeatureQueryResponse>>
    {
        public Task<IEnumerable<GetByIdFeatureQueryResponse>> Handle(GetByIdFeatureQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
