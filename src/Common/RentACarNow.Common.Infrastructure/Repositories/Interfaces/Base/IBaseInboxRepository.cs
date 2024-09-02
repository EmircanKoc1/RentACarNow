using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.RepositoryEnums;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base
{
    public interface IBaseInboxRepository<TInboxMessage>
        where TInboxMessage : BaseInboxMessage
    {
        Task AddMessageAsync(TInboxMessage message);

        Task<IEnumerable<TInboxMessage>> GetMessagesAsync(int messageCount, OrderedDirection direction);

        Task<TInboxMessage> GetMessageByIdAsync(Guid messageId);

        Task MarkMessagesProccessedAsync(IEnumerable<Guid> ids, DateTime proccessedDate);
        Task MarkMessageProccessedAsync(Guid id, DateTime proccessedDate);

        //Task DeleteMessageAsync(Guid messageId);
        Task DeleteMessagesAsync(IEnumerable<Guid> ids);


    }
}
