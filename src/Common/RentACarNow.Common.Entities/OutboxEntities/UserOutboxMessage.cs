using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.OutboxEntities
{
    public sealed class UserOutboxMessage : BaseOutboxMessage
    {
        public UserEventType EventType { get; set; }

    }


}
