namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromCustomer
{
    public class DeleteClaimFromCustomerCommandRequest
    {
        public Guid AdminId { get; set; }
        public Guid ClaimId { get; set; }

    }
}
