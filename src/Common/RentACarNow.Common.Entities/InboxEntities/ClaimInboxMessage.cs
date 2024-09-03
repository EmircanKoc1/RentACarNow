using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.InboxEntities
{
    public class ClaimInboxMessage : BaseInboxMessage
    {
        public ClaimEventType EventType { get; set; }

    }
}
