using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Events.User
{
    public sealed class UserUpdatedEvent : BaseEvent
    {
        public Guid UserId { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public int Age { get; init; }
        public string PhoneNumber { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public decimal WalletBalance { get; init; }
        public DateTime UpdatedDate { get; init; }
    }
}
