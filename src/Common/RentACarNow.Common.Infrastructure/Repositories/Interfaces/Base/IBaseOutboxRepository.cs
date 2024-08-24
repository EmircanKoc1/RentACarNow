using MongoDB.Driver;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.RepositoryEnums;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base
{
    public interface IBaseOutboxRepository<TOutboxMessage>
        where TOutboxMessage : BaseOutboxMessage
    {
        Task AddMessageAsync(TOutboxMessage message, IClientSessionHandle session);
        Task<IEnumerable<TOutboxMessage>> GetOutboxMessagesAsync(int messageCount, OrderedDirection direction);

        Task MarkPublishedMessagesAsync(IEnumerable<Guid> ids, DateTime date, IClientSessionHandle sesison);
        Task<IClientSessionHandle> StartSessionAsync();

    }
}
