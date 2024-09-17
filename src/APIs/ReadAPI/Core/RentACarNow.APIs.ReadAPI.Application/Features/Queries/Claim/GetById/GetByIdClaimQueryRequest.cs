using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById
{
    public class GetByIdClaimQueryRequest : IRequest<ResponseWrapper<GetByIdClaimQueryResponse>>
    {
        public Guid ClaimId { get; set; }
    }

}
