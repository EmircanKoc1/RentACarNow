using RentACarNow.Domain.Events.Common;
using RentACarNow.Domain.Events.Common.Messages;

namespace RentACarNow.Domain.Events.Brand
{
    public class BrandUpdatedEvent : BaseEvent
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<CarMessage> Cars { get; set; }

    }

}
