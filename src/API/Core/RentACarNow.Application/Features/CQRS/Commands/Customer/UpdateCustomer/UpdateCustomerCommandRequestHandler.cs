using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandRequestHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        public Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada müşteri güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

}
