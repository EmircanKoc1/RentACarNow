using MongoDB.Driver;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.RepositoryEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base
{
    public interface IBaseOutboxRepository<TOutboxMessage> 
        where TOutboxMessage : BaseOutboxMessage
    {
        Task AddMessageAsync(TOutboxMessage message);
        Task<IEnumerable<TOutboxMessage>> GetOutboxMessagesAsync(int messageCount, OrderedDirection direction);

        Task MarkPublishedMessagesAsync(IEnumerable<Guid> ids, DateTime date);
        Task<IClientSessionHandle> StartSessionAsync();

    }
}
