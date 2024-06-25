using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.Persistence.Repositories.Base
{
    public abstract class MongoBaseWriteRepository<TEntity> : IMongoWriteRepository<TEntity>
        where TEntity : BaseEntity, IMongoEntity
    {
    }
}
