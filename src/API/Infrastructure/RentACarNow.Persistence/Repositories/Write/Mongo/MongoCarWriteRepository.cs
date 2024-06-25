using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Write.Mongo
{
    public class MongoCarWriteRepository : MongoBaseWriteRepository<Car>, IMongoAdminWriteRepository
    {

    }
}
