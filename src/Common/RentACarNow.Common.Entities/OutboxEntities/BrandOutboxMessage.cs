using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.OutboxEntities
{
    public sealed class BrandOutboxMessage : BaseOutboxMessage
    {
        public BrandEventType EventType { get; set; }

    }
}
