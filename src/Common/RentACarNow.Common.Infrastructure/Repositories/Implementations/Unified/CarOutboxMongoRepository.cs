using RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Implementations;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;


namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified
{
    public class CarOutboxMongoRepository : BaseOutboxMongoRepository<CarOutboxMessage, MongoCarOutboxContext>, ICarOutboxRepository
    {
        public CarOutboxMongoRepository(MongoCarOutboxContext context) : base(context)
        {
        }
    }
}
