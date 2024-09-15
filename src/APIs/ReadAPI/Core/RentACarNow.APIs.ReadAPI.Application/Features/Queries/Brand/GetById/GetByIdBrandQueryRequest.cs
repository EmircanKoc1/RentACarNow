using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById
{
    public class GetByIdBrandQueryRequest : IRequest<IEnumerable<GetByIdBrandQueryResponse>>
    {
        public Guid BrandId { get; set; }
    }

}
