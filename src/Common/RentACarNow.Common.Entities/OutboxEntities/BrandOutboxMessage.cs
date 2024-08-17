using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.OutboxEntities
{
    public sealed class BrandOutboxMessage : BaseOutboxMessage
    {
        BrandEventType EventType { get; set; }

    }
}
