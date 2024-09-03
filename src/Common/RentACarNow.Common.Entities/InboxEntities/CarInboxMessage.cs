using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.InboxEntities
{
    public class CarInboxMessage : BaseInboxMessage
    {
        public CarEventType EventType { get; set; }
    }
}
