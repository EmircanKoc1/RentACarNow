using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetById
{
    public class GetByIdCustomerQueryRequestHandler : IRequestHandler<GetByIdCustomerQueryRequest, IEnumerable<GetByIdCustomerQueryResponse>>
    {
        public Task<IEnumerable<GetByIdCustomerQueryResponse>> Handle(GetByIdCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
