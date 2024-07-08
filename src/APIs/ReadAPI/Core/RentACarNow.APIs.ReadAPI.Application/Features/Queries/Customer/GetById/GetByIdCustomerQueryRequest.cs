using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetById
{
    public class GetByIdCustomerQueryRequest : IRequest<IEnumerable<GetByIdCustomerQueryResponse>>
    {
        public Guid Id { get; set; }

    }

}
