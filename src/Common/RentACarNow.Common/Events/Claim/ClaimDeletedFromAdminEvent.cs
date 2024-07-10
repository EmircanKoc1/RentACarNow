namespace RentACarNow.Common.Events.Claim
{
    public class ClaimDeletedFromAdminEvent
    {
        public Guid ClaimId { get; set; }
        public Guid AdminId { get; set; }

    }
}
