using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.InboxEntities
{
    public class RentalInboxMessage : BaseInboxMessage
    {
        public RentalEventType EventType { get; set; }

    }
}
