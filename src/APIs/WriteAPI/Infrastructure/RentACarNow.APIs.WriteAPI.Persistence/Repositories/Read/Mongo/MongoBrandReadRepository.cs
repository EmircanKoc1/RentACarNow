using RentACarNow.APIs.WriteAPI.Persistence.Contexts.MongoContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.Mongo
{
    public class MongoBrandReadRepository : MongoBaseReadRepository<Brand>, IMongoBrandReadRepository
    {
        public MongoBrandReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }

}
