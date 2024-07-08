using MediatR;
using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll
{
    public class GetAllBrandQueryRequest : IRequest<IEnumerable<GetAllBrandQueryResponse>>
    {
        public PaginationParameter PaginationParameter { get; set; }

        public OrderingParameter OrderingParameter { get; set; }
    }

}
