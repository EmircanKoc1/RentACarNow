using RentACarNow.Common.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RentACarNow.Common.Events.Feature
{
    public class FeatureDeletedEvent : BaseEvent
    {
        public Guid FeatureId { get; set; }
        public Guid CarId { get; set; }
    }
}
