using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Brand
{
    public sealed class BrandUpdatedEvent : BaseEvent
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDate { get; set; }

    }

}
