using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.InboxEntities
{
    public class UserInboxMessage : BaseInboxMessage
    {
        public UserEventType EventType { get; set; }

    }
}
