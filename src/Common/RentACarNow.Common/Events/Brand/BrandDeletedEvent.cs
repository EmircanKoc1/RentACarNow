using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Brand
{
    public class BrandDeletedEvent : BaseEvent
    {
        public Guid id { get; set; }
    }
}
