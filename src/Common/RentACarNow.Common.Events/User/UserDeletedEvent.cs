using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserDeletedEvent : BaseEvent
    {
        public Guid UserId { get; init; }
        public DateTime DeletedDate { get; set; }

    }



}
