using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo
{
    public class MongoUserReadRepository : MongoBaseReadRepository<User>, IMongoUserReadRepository
    {
        public MongoUserReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }

}
