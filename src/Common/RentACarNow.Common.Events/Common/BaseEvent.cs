using Microsoft.VisualBasic;

namespace RentACarNow.Common.Events.Common
{
    //public record BaseEvent(DateTime? CreatedDate, DateTime? UpdatedDate, DateTime? DeletedDate) : IEvent;

    public class BaseEvent : IEvent
    {
        public BaseEvent()
        {
            MessageId = Guid.NewGuid();
        }

        public Guid MessageId { get; set; }
    }
}
