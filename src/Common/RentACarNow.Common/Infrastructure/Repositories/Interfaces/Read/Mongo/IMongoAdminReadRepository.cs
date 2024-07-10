using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo
{
    public interface IMongoAdminReadRepository : IMongoReadRepository<Admin>
    {
        Task<Admin> GetAdminByLogin(string usernameOrEmail, string password);


    }


}
