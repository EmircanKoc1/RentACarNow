using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.DeleteCustomer
{
    public class DeleteCustomerCommandRequest : IRequest<DeleteCustomerCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
