using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.OutboxEntities
{
    public class ClaimOutboxMessage : BaseOutboxMessage
    {
        public ClaimEventType ClaimEventType { get; set; }
    }
}
