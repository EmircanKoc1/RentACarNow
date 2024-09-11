using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserDeletedEvent : BaseEvent
    {
        public UserDeletedEvent(Guid userId, DateTime deletedDate)
        {
            UserId = userId;
            DeletedDate = deletedDate;
        }

        public Guid UserId { get; init; }
        public DateTime DeletedDate { get; set; }

    }



}
