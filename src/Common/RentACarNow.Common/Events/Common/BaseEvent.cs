using Microsoft.VisualBasic;

namespace RentACarNow.Domain.Events.Common
{
    //public record BaseEvent(DateTime? CreatedDate, DateTime? UpdatedDate, DateTime? DeletedDate) : IEvent;

    public class BaseEvent : IEvent
    {
        public DateTime? CreatedDate { get; set; } 
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
