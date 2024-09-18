using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Base;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetAll
{
    public class GetAllUserQueryRequest : BaseGetAllQueryRequest, IRequest<ResponseWrapper<IEnumerable<GetAllUserQueryResponse>>>
    {


    }

}
