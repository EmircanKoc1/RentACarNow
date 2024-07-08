using MediatR;
using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Employee.GetAll
{
    public class GetAllEmployeeQueryRequest : IRequest<IEnumerable<GetAllEmployeeQueryResponse>>
    {
        public PaginationParameter PaginationParameter { get; set; }

        public OrderingParameter OrderingParameter { get; set; }

    }

}
