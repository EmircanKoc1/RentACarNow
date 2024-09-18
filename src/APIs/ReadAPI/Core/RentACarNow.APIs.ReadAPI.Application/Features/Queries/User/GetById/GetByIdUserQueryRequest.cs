using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetById
{
    public class GetByIdUserQueryRequest : IRequest<ResponseWrapper<GetByIdUserQueryResponse>>
    {
        public Guid UserId { get; set; }
    }


}
