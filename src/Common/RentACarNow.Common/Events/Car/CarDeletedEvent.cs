﻿using RentACarNow.Common.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Car
{
    public class CarDeletedEvent : BaseEvent
    {
        public Guid Id { get; set; }
    }
}
