using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo
{
    public interface IMongoAdminReadRepository : IMongoReadRepository<Admin>
    {
    }


}
