using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.Admin
{
    public class AdminDeletedEvent : BaseEvent
    {
        public Guid Id { get; set; }
    }


}
