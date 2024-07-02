using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoBrandWriteRepository : MongoBaseWriteRepository<Brand>, IMongoBrandWriteRepository
    {
        public MongoBrandWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
