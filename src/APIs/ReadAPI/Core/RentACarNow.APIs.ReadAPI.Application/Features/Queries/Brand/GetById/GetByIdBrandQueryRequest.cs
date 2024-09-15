using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById
{
    public class GetByIdBrandQueryRequest : IRequest<ResponseWrapper<GetByIdBrandQueryResponse>>
    {
        public Guid BrandId { get; set; }
    }

}
