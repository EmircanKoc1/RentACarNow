using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandRequestHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        public Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada müşteri oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

}
