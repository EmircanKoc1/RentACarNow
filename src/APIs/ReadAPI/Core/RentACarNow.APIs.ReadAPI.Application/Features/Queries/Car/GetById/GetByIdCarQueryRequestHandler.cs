using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById
{
    public class GetByIdCarQueryRequestHandler : IRequestHandler<GetByIdCarQueryRequest, IEnumerable<GetByIdCarQueryResponse>>
    {
        public Task<IEnumerable<GetByIdCarQueryResponse>> Handle(GetByIdCarQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
