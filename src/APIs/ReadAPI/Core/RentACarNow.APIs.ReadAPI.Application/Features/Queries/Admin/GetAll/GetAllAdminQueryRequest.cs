using MediatR;
using RentACarNow.APIs.ReadAPI.Application.DTOs;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll
{
    public class GetAllAdminQueryRequest : IRequest<IEnumerable<GetAllAdminQueryResponse>>
    {

        public PaginationDTO PaginationParameters { get; set; }



    }

}
