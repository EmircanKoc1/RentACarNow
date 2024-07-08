using MediatR;
using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetAll
{
    public class GetAllFeatureQueryRequest : IRequest<IEnumerable<GetAllFeatureQueryResponse>>
    {
        public PaginationParameter PaginationParameter { get; set; }

        public OrderingParameter OrderingParameter { get; set; }

    }

}
