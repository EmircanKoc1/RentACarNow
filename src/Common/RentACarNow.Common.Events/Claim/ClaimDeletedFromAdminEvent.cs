using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimDeletedFromAdminEvent : BaseEvent
    {
        public Guid ClaimId { get; set; }
        public Guid AdminId { get; set; }

    }
}
