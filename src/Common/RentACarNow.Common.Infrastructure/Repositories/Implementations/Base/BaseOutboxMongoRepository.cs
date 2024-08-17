using MongoDB.Driver;
using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Base
{
    public abstract class BaseOutboxMongoRepository<TOutboxMessage, TOutboxContext> : IBaseOutboxMongoRepository<TOutboxMessage>
        where TOutboxMessage : BaseOutboxMessage
        where TOutboxContext : IBaseMongoOutboxContext<TOutboxMessage>
    {

        protected readonly TOutboxContext _context;

        protected BaseOutboxMongoRepository(TOutboxContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(TOutboxMessage message)
            => await _context.GetCollection.InsertOneAsync(message);


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
                                      .ToListAsync();

        }

        public async Task MarkPublishedMessageAsync(Guid id, DateTime date)
        {
            var filterDefinition = Builders<TOutboxMessage>.Filter
                .Eq(om => om.Id, id);

            var updateDefinition = Builders<TOutboxMessage>.Update
                .Set(om => om.IsPublished, true)
                .Set(om => om.PublishDate, date);

            await _context.GetCollection.UpdateManyAsync(filterDefinition, updateDefinition);
        }
    }
}
