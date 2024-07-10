using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo
{
    public interface IMongoCustomerReadRepository : IMongoReadRepository<Customer>
    {
        Task<Customer> GetCustomerByLogin(string usernameOrEmail, string password);

    }


}
