namespace RentACarNow.Common.Events.Common
{

    public class BaseEvent : IEvent
    {
       
        public Guid MessageId { get; set; }

        public T SetMessageId<T>(Guid messageId) where T : BaseEvent
        {
            MessageId = messageId;

            return (T)this;
        }

    }
}
