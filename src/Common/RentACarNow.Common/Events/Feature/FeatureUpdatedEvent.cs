using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Feature
{
    public class FeatureUpdatedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public CarMessage Car { get; set; }
    }
}
