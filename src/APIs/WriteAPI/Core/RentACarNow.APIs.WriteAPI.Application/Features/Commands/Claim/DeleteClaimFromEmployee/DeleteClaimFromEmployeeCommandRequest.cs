using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromEmployee
{
    public class DeleteClaimFromEmployeeCommandRequest : IRequest<DeleteClaimFromEmployeeCommandResponse>
    {
        public Guid EmployeeId { get; set; }
        public Guid ClaimId { get; set; }

    }
}
