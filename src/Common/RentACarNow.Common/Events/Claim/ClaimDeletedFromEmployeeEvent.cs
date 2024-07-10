namespace RentACarNow.Common.Events.Claim
{
    public class ClaimDeletedFromEmployeeEvent
    {
        public Guid ClaimId { get; set; }
        public Guid EmployeeId { get; set; }

    }
}
