using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Write.Mongo
{
    public class MongoCustomerWriteRepository : MongoBaseWriteRepository<Customer>, IMongoCustomerWriteRepository
    {
        public MongoCustomerWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
