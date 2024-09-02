using Microsoft.VisualBasic;

namespace RentACarNow.Common.Events.Common
{
    //public record BaseEvent(DateTime? CreatedDate, DateTime? UpdatedDate, DateTime? DeletedDate) : IEvent;

    public class BaseEvent : IEvent
    {
        public Guid IdempotentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
