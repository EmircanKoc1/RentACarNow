using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Brand
{
    public class BrandAddedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CarMessage> Cars { get; set; }
    }

}
