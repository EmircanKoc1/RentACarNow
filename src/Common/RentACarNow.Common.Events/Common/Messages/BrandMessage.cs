﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Common.Messages
{
    public class BrandMessage
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
