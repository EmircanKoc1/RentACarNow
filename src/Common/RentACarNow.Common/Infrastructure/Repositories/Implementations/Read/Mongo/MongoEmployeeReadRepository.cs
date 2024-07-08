using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo
{
    public class MongoEmployeeReadRepository : MongoBaseReadRepository<Employee>, IMongoEmployeeReadRepository
    {
        public MongoEmployeeReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }


}
