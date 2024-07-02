using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoCarWriteRepository : MongoBaseWriteRepository<Car>, IMongoCarWriteRepository
    {
        public MongoCarWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
