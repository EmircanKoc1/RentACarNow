using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToCustomer
{
    public class AddClaimToCustomerCommandRequest : IRequest<AddClaimToCustomerCommandResponse>
    {
        public Guid CustomerId { get; set; }
        public Guid ClaimId { get; set; }
    }
}
