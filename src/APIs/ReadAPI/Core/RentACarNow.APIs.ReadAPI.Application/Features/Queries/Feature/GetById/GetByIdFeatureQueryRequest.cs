using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetById
{
    public class GetByIdFeatureQueryRequest : IRequest<IEnumerable<GetByIdFeatureQueryResponse>>
    {
        public Guid Id { get; set; }

    }

}
