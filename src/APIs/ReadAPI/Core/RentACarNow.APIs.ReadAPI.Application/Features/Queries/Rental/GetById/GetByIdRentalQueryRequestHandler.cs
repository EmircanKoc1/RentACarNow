using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById
{
    public class GetByIdRentalQueryRequestHandler : IRequestHandler<GetByIdRentalQueryRequest, IEnumerable<GetByIdRentalQueryResponse>>
    {
        public Task<IEnumerable<GetByIdRentalQueryResponse>> Handle(GetByIdRentalQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
