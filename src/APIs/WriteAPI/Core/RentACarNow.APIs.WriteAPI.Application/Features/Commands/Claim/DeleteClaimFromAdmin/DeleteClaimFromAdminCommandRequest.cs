using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromAdmin
{
    public class DeleteClaimFromAdminCommandRequest : IRequest<DeleteClaimFromAdminCommandResponse>
    {
        public Guid AdminId { get; set; }
        public Guid ClaimId { get; set; }

    }
}
