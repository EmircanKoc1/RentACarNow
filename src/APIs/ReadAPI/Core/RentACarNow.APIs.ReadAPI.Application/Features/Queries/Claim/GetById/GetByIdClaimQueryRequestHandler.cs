using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById
{
    public class GetByIdClaimQueryRequestHandler : IRequestHandler<GetByIdClaimQueryRequest, IEnumerable<GetByIdClaimQueryResponse>>
    {
        public Task<IEnumerable<GetByIdClaimQueryResponse>> Handle(GetByIdClaimQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
