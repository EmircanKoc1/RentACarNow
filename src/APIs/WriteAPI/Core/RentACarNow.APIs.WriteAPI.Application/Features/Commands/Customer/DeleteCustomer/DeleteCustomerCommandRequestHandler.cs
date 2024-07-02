using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.DeleteCustomer
{
    public class DeleteCustomerCommandRequestHandler : IRequestHandler<DeleteCustomerCommandRequest, DeleteCustomerCommandResponse>
    {
        public Task<DeleteCustomerCommandResponse> Handle(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada müşteri silme işleminin kodunu yazmanız gerekecek
        }
    }

}
