using MongoDB.Driver;
using RentACarNow.Common.Contexts.InboxContexts.Interfaces;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Base
{
    public abstract class BaseInboxMongoRepository<TInboxMessage, TInboxContext> : IBaseInboxRepository<TInboxMessage>
        where TInboxMessage : BaseInboxMessage
        where TInboxContext : IBaseMongoInboxContext<TInboxMessage>
    {
        protected readonly TInboxContext _context;

        protected BaseInboxMongoRepository(TInboxContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(TInboxMessage message)
        {
            await _context.GetCollection.InsertOneAsync(message);
        }


        public async Task DeleteMessagesAsync(IEnumerable<Guid> ids)
        {
            var filterDefinition = Builders<TInboxMessage>.Filter
                .In(im => im.MessageId, ids);

            await _context.GetCollection.DeleteManyAsync(filterDefinition);
        }

        public async Task<TInboxMessage> GetMessageByIdAsync(Guid messageId)
        {
            var filterDefinition = Builders<TInboxMessage>.Filter
                .Eq(im => im.MessageId, messageId);

            return await _context.GetCollection.Find(filterDefinition).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TInboxMessage>> GetMessagesAsync(int messageCount, OrderedDirection direction)
        {
            var filterDefinition = Builders<TInboxMessage>.Filter
            .Eq(om => om.IsProcessed, false);

            SortDefinition<TInboxMessage>? sortDefinition = default;

            if (direction is not OrderedDirection.None)
            {
                sortDefinition = direction switch
                {
                    OrderedDirection.Ascending => Builders<TInboxMessage>.Sort
                    .Ascending(om => om.AddedDate),
                    OrderedDirection.Descending => Builders<TInboxMessage>.Sort
                    .Descending(om => om.AddedDate),
                    _ => throw new Exception("")
                };
            }

            return await _context.GetCollection
                                      .Find(filterDefinition)
                                      .Sort(sortDefinition)
                                      .Limit(messageCount)
                                      .ToListAsync();
        }

        public async Task MarkMessagesProccessedAsync(IEnumerable<Guid> ids, DateTime proccessedDate)
        {

            var filterDefinition = Builders<TInboxMessage>.Filter
            .In(om => om.MessageId, ids);

            var updateDefinition = Builders<TInboxMessage>.Update
                .Set(om => om.IsProcessed, true)
                .Set(om => om.ProcessedDate, proccessedDate);

            await _context.GetCollection.UpdateManyAsync(filterDefinition, updateDefinition);
        }
        public async Task MarkMessageProccessedAsync(Guid id, DateTime proccessedDate)
        {

            var filterDefinition = Builders<TInboxMessage>.Filter
            .Eq(om => om.MessageId, id);

            var updateDefinition = Builders<TInboxMessage>.Update
                .Set(om => om.IsProcessed, true)
                .Set(om => om.ProcessedDate, proccessedDate);

            await _context.GetCollection.UpdateManyAsync(filterDefinition, updateDefinition);
        }

    }
}
