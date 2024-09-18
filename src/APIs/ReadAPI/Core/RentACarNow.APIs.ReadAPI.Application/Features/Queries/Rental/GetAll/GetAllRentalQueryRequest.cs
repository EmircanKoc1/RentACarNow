using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Base;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll
{
    public class GetAllRentalQueryRequest : BaseGetAllQueryRequest, IRequest<ResponseWrapper<IEnumerable<GetAllRentalQueryResponse>>>
    {

    }

}
