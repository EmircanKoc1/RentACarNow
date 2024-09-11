using RentACarNow.Common.Events.User;

namespace RentACarNow.Common.Infrastructure.Factories.Interfaces
{
    public interface IUserEventFactory
    {
        UserCreatedEvent CreateUserCreatedEvent(
            Guid userId,
            string name,
            string surname,
            int age,
            string phoneNumber,
            string username,
            string email,
            string password,
            decimal walletBalance,
            DateTime createdDate);

        UserUpdatedEvent CreateUserUpdatedEvent(
            Guid userId,
            string name,
            string surname,
            int age,
            string phoneNumber,
            string username,
            string email,
            string password,
            decimal walletBalance,
            DateTime updatedDate);

        UserDeletedEvent CreateUserDeletedEvent(Guid userId, DateTime deletedDate);
        UserClaimAddedEvent CreateUserClaimAddedEvent(Guid userId, Guid claimId, string key, string value);
        UserClaimUpdatedEvent CreateUserClaimUpdatedEvent(Guid claimId, string key, string value);
        UserClaimDeletedEvent CreateUserClaimDeletedEvent(Guid userId, Guid claimId);
    }
}
