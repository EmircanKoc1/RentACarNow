using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimDeletedFromEmployeeEvent : BaseEvent
    {
        public Guid ClaimId { get; set; }
        public Guid EmployeeId { get; set; }

    }
}
