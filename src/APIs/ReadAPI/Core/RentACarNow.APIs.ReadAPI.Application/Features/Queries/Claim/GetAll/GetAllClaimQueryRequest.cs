using MediatR;
using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll
{
    public class GetAllClaimQueryRequest : IRequest<IEnumerable<GetAllClaimQueryResponse>>
    {
        public PaginationParameter PaginationParameter { get; set; }

        public OrderingParameter OrderingParameter { get; set; }

    }

}
