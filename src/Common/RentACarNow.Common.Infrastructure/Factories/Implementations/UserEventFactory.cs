using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;

namespace RentACarNow.Common.Infrastructure.Factories.Implementations
{
    public class UserEventFactory : IUserEventFactory
    {
        public UserCreatedEvent CreateUserCreatedEvent(
            Guid userId,
            string name,
            string surname,
            int age,
            string phoneNumber,
            string username,
            string email,
            string password,
            decimal walletBalance,
            DateTime createdDate)
        {
            return new UserCreatedEvent(
                userId,
                name,
                surname,
                age,
                phoneNumber,
                username,
                email,
                password,
                walletBalance,
                createdDate);
        }

        public UserUpdatedEvent CreateUserUpdatedEvent(
            Guid userId,
            string name,
            string surname,
            int age,
            string phoneNumber,
            string username,
            string email,
            string password,
            decimal walletBalance,
            DateTime updatedDate)
        {
            return new UserUpdatedEvent(
                userId,
                name,
                surname,
                age,
                phoneNumber,
                username,
                email,
                password,
                walletBalance,
                updatedDate);
        }

        public UserDeletedEvent CreateUserDeletedEvent(Guid userId, DateTime deletedDate)
        {
            return new UserDeletedEvent(userId, deletedDate);
        }

        public UserClaimAddedEvent CreateUserClaimAddedEvent(Guid userId, Guid claimId, string key, string value)
        {
            var claimMessage = new ClaimMessage
            {
                ClaimId = claimId,
                Key = key,
                Value = value
            };

            return new UserClaimAddedEvent(userId, claimMessage);
        }

        public UserClaimUpdatedEvent CreateUserClaimUpdatedEvent(Guid claimId, string key, string value)
        {
            return new UserClaimUpdatedEvent(claimId, key, value);
        }

        public UserClaimDeletedEvent CreateUserClaimDeletedEvent(Guid userId, Guid claimId)
        {
            return new UserClaimDeletedEvent(userId, claimId);
        }
    }
}
