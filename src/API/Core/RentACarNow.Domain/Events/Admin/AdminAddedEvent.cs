﻿using RentACarNow.Domain.Events.Common;
using RentACarNow.Domain.Events.Common.Messages;

namespace RentACarNow.Domain.Events.Admin
{
    public class AdminAddedEvent : BaseEvent
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<ClaimMessage> Claims { get; set; }

    }




}