using MediatR;
using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetAll
{
    public class GetAllCustomerQueryRequest : IRequest<IEnumerable<GetAllCustomerQueryResponse>>
    {
        public PaginationParameter PaginationParameter { get; set; }

        public OrderingParameter OrderingParameter { get; set; }

    }

}
