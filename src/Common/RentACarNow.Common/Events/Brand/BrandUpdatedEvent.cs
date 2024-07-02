using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Brand
{
    public class BrandUpdatedEvent : BaseEvent
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<CarMessage> Cars { get; set; }

    }

}
