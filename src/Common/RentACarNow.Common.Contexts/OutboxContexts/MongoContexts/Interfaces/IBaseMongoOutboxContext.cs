using MongoDB.Driver;
using RentACarNow.Common.Entities.OutboxEntities;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces
{
    public interface IBaseMongoOutboxContext<TEntity>
        where TEntity : IMongoOutbox
    {
        IMongoCollection<TEntity> GetCollection { get; } 

    }
}
