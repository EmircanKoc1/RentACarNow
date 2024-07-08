using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo
{
    public interface IMongoEmployeeReadRepository : IMongoReadRepository<Employee>
    {
    }


}
