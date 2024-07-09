using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromCustomer
{
    public class DeleteClaimFromCustomerCommandRequest : IRequest<DeleteClaimFromCustomerCommandResponse>
    {
        public Guid CustomerId { get; set; }
        public Guid ClaimId { get; set; }

    }
}
