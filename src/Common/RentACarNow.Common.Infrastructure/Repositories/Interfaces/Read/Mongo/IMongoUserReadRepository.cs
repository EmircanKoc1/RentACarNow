using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo
{
    public interface IMongoUserReadRepository : IMongoReadRepository<User>
    {
    }


}
