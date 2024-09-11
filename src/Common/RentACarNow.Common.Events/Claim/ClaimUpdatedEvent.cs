using RentACarNow.Common.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimUpdatedEvent : BaseEvent,IEvent
    {
        public ClaimUpdatedEvent(Guid claimId, string key, string value, DateTime claimUpdatedDate)
        {
            ClaimId = claimId;
            Key = key;
            Value = value;
            ClaimUpdatedDate = claimUpdatedDate;
        }

        public Guid ClaimId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime ClaimUpdatedDate { get; set; }

    }
}
