using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserDeletedEvent  : BaseEvent
    {
        public Guid Id { get; init; }
    }



}
