using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById
{
    public class GetByIdClaimQueryRequest : IRequest<IEnumerable<GetByIdClaimQueryResponse>>
    {
        public Guid Id { get; set; }
    }

}
