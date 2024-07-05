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
        public Guid Id { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

    }
}
