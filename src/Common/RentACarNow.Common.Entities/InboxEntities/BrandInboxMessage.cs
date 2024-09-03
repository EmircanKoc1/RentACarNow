using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.InboxEntities
{
    public class BrandInboxMessage : BaseInboxMessage
    {
        public BrandEventType EventType { get; set; }

    }
}
