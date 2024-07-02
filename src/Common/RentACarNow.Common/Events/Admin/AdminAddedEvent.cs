using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Admin
{
    public class AdminAddedEvent : BaseEvent
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<ClaimMessage> Claims { get; set; }

    }




}
