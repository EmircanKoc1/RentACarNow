using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoCustomerWriteRepository : MongoBaseWriteRepository<Customer>, IMongoCustomerWriteRepository
    {
        public MongoCustomerWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
