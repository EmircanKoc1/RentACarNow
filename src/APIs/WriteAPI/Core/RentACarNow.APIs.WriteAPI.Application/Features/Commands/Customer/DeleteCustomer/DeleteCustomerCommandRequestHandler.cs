using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.DeleteCustomer
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
