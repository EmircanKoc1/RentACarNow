using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Brand
{
    public class BrandDeletedEvent : BaseEvent
    {
        public Guid Id { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}
