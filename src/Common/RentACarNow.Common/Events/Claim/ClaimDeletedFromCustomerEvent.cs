﻿using RentACarNow.Common.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimDeletedFromCustomerEvent : BaseEvent
    {
        public Guid ClaimId { get; set; }
        public Guid CustomerId { get; set; }

    }
}