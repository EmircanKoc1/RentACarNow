using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToAdmin
{
    public class AddClaimToAdminCommandRequest : IRequest<AddClaimToAdminCommandResponse>
    {
        public Guid AdminId { get; set; }
        public Guid ClaimId { get; set; }
    }
}
