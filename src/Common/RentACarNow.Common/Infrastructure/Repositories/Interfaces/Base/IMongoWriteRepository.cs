using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base
{
    public interface IMongoWriteRepository<TEntity> : IBaseWriteRepository<TEntity>
        where TEntity : MongoBaseEntity, IMongoEntity
    {
    }
}
