using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Base;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll
{
    public class GetAllClaimQueryRequest : BaseGetAllQueryRequest, IRequest<ResponseWrapper<IEnumerable<GetAllClaimQueryResponse>>>
    {


    }

}
