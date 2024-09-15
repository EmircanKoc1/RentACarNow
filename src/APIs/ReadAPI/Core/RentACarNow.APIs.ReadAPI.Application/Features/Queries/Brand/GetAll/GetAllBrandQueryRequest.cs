using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Base;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll
{
    public class GetAllBrandQueryRequest : BaseGetAllQueryRequest, IRequest<ResponseWrapper<IEnumerable<GetAllBrandQueryResponse>>>
    {


    }

}
