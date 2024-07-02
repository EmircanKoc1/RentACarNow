using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo
{
    public class MongoCarReadRepository : MongoBaseReadRepository<Car>, IMongoCarReadRepository
    {
        public MongoCarReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }

}
