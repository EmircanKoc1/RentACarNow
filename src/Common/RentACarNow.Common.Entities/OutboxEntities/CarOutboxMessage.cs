using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.OutboxEntities
{
    public sealed class CarOutboxMessage : BaseOutboxMessage
    {
        public CarEventType CarEventType { get; set; }
    }
}
