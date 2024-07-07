using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToEmployee
{
    public class AddClaimToEmployeeCommandRequest : IRequest<AddClaimToEmployeeCommandResponse>
    {
        public Guid EmployeeId { get; set; }
        public Guid ClaimId { get; set; }
    }
}
