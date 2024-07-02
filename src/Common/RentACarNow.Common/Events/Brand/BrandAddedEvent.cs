using RentACarNow.Domain.Enums;
using RentACarNow.Domain.Events.Common;
using RentACarNow.Domain.Events.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Domain.Events.Brand
{
    public class BrandAddedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<CarMessage> Cars { get; set; }
    }

}
