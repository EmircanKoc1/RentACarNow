using RentACarNow.Domain.Events.Common;

namespace RentACarNow.Domain.Events.Brand
{
    public class BrandDeletedEvent : BaseEvent
    {
        public Guid id { get; set; }
    }
}
