namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromEmployee
{
    public class DeleteClaimFromEmployeeCommandRequest
    {
        public Guid AdminId { get; set; }
        public Guid ClaimId { get; set; }

    }
}
