using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Events.Brand
{
    public class BrandAddedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<CarMessage> Cars { get; set; }
    }

}
