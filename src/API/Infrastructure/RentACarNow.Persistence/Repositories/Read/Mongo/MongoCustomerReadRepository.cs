using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Read.Mongo
{
    public class MongoCustomerReadRepository : MongoBaseReadRepository<Customer>, IMongoCustomerReadRepository
    {
        public MongoCustomerReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }

}
