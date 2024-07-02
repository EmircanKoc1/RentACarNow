using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoRentalWriteRepository : MongoBaseWriteRepository<Rental>, IMongoRentalWriteRepository
    {
        public MongoRentalWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
