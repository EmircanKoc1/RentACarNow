﻿using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Claim
{
    public class ClaimAddedEvent : BaseEvent
    {
        public Guid Id { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

    }
}
