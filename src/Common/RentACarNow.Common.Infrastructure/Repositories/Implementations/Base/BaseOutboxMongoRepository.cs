using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Base
{
    public abstract class BaseOutboxMongoRepository<TOutboxMessage, TOutboxContext> : IBaseOutboxRepository<TOutboxMessage>
        where TOutboxMessage : BaseOutboxMessage
        where TOutboxContext : IBaseMongoOutboxContext<TOutboxMessage>
    {

        protected readonly TOutboxContext _context;

        protected BaseOutboxMongoRepository(TOutboxContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(TOutboxMessage message, IClientSessionHandle session)
            => await _context.GetCollection.InsertOneAsync(session, message);


        public async Task<IEnumerable<TOutboxMessage>> GetOutboxMessagesAsync(int messageCount, OrderedDirection direction)
        {
            var filterDefinition = Builders<TOutboxMessage>.Filter
                .Eq(om => om.IsPublished, false);

            SortDefinition<TOutboxMessage>? sortDefinition = default;

            if (direction is not OrderedDirection.None)
            {
                sortDefinition = direction switch
                {
                    OrderedDirection.Ascending => Builders<TOutboxMessage>.Sort
                    .Ascending(om => om.AddedDate),

                    OrderedDirection.Descending => Builders<TOutboxMessage>.Sort
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

        public async Task MarkPublishedMessageAsync(Guid id, DateTime? date)
        {
            date ??= DateTime.Now;

            var filterDefinition = Builders<TOutboxMessage>
                .Filter
                .Eq(om => om.Id, id);

            var updateDefinition = Builders<TOutboxMessage>.Update
                .Set(om => om.IsPublished, true)
                .Set(om => om.PublishDate, date);

            await _context.GetCollection.UpdateManyAsync(filterDefinition, updateDefinition);
        }

        public async Task MarkPublishedMessagesAsync(IEnumerable<Guid> ids, DateTime? date, IClientSessionHandle session)
        {
            date ??= DateTime.Now;


            var filterDefinition = Builders<TOutboxMessage>.Filter
                .In(om => om.Id, ids);

            var updateDefinition = Builders<TOutboxMessage>.Update
                .Set(om => om.IsPublished, true)
                .Set(om => om.PublishDate, date);

            await _context.GetCollection.UpdateManyAsync(session, filterDefinition, updateDefinition);
        }

        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            return await _context.StartSessionAsync();
        }
    }
}
