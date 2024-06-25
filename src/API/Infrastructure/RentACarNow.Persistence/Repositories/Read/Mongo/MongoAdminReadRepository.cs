using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Read.Mongo
{

    public class MongoAdminReadRepository : MongoBaseReadRepository<Admin>, IMongoAdminReadRepository
    {
    }
   
}
