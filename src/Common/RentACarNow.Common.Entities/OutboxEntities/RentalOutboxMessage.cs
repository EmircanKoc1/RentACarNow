using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;

namespace RentACarNow.Common.Entities.OutboxEntities
{
    public sealed class RentalOutboxMessage : BaseOutboxMessage
    {

        public RentalEventType EventType { get; set; }
    }

}
