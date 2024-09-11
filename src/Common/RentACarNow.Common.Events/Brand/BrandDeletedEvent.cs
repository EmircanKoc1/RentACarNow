using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Brand
{
    public sealed class BrandDeletedEvent : BaseEvent
    {
        public BrandDeletedEvent(
            Guid brandId,
            DateTime deletedDate)
        {
            BrandId = brandId;
            DeletedDate = deletedDate;
        }

        public Guid BrandId { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}
