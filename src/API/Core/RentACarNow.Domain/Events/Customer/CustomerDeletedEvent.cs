using RentACarNow.Domain.Events.Common;

namespace RentACarNow.Domain.Events.Customer
{
    public class CustomerDeletedEvent : BaseEvent
    {
        public Guid Id { get; set; }
    }
}
