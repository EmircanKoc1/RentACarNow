using MediatR;
using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll
{
    public class GetAllRentalQueryRequest : IRequest<IEnumerable<GetAllRentalQueryResponse>>
    {
        public PaginationParameter PaginationParameter { get; set; }

        public OrderingParameter OrderingParameter { get; set; }
    }

}
