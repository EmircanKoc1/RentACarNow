using RentACarNow.Domain.Events.Common;
using RentACarNow.Domain.Events.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Domain.Events.Feature
{
    public class FeatureUpdatedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public CarMessage Car { get; set; }
    }
}
