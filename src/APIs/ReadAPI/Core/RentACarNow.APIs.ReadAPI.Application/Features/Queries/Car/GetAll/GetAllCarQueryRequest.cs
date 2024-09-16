using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Base;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll
{
    public class GetAllCarQueryRequest : BaseGetAllQueryRequest, IRequest<ResponseWrapper<IEnumerable<GetAllCarQueryResponse>>>
    {


    }

}
