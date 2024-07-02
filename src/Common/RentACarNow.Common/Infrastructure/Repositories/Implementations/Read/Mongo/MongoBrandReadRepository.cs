using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo
{
    public class MongoBrandReadRepository : MongoBaseReadRepository<Brand>, IMongoBrandReadRepository
    {
        public MongoBrandReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }

}
