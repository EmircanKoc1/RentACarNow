using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoCustomerWriteRepository : MongoBaseWriteRepository<Customer>, IMongoCustomerWriteRepository
    {
        public MongoCustomerWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
