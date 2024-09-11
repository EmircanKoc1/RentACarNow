using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Brand
{
    public sealed class BrandDeletedEvent : BaseEvent
    {
        public Guid BrandId { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}
