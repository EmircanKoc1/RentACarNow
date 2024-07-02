using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Customer
{
    public class CustomerDeletedEvent : BaseEvent
    {
        public Guid Id { get; set; }
    }
}
