using RentACarNow.Domain.Events.Common;

namespace RentACarNow.Domain.Events.Admin
{
    public class AdminDeletedEvent : BaseEvent
    {
        public  Guid Id { get; set; }
    }


}
