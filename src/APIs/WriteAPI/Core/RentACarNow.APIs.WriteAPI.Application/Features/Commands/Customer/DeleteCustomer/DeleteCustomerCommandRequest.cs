using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.DeleteCustomer
{
    public class DeleteCustomerCommandRequest : IRequest<DeleteCustomerCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
