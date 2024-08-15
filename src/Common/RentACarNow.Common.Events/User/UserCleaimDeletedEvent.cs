namespace RentACarNow.Common.Events.User
{
    public sealed class UserCleaimDeletedEvent
    {
        public Guid UserId { get; init; }
        public Guid ClaimId { get; init; }

    }
}
