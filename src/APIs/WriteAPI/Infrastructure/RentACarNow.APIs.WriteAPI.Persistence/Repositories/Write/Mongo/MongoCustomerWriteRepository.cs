using RentACarNow.APIs.WriteAPI.Persistence.Contexts.MongoContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.Mongo
{
    public class MongoCustomerWriteRepository : MongoBaseWriteRepository<Customer>, IMongoCustomerWriteRepository
    {
        public MongoCustomerWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
