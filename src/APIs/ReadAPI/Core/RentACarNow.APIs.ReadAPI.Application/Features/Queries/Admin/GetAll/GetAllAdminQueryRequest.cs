using MediatR;
using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll
{
    public class GetAllAdminQueryRequest : IRequest<IEnumerable<GetAllAdminQueryResponse>>
    {

        public PaginationParameter PaginationParameter { get; set; }

        public OrderingParameter OrderingParameter { get; set; }




    }

}
