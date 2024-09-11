using RentACarNow.Common.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimDeletedEvent : BaseEvent
    {
        public ClaimDeletedEvent(Guid claimId, DateTime claimDeletedDate)
        {
            ClaimId = claimId;
            ClaimDeletedDate = claimDeletedDate;
        }

        public Guid ClaimId { get; set; }

        public DateTime ClaimDeletedDate { get; set; }
    }
}
