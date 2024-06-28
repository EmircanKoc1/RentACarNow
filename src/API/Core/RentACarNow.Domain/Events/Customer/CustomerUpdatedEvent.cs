using RentACarNow.Domain.Events.Common;
using RentACarNow.Domain.Events.Common.Messages;

namespace RentACarNow.Domain.Events.Customer
{
    public class CustomerUpdatedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }

        public ICollection<ClaimMessage> Claims { get; set; }

    }
}
