using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Read.Mongo
{
    public class MongoRentalReadRepository : MongoBaseReadRepository<Rental>, IMongoRentalReadRepository
    {
        public MongoRentalReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }

}
