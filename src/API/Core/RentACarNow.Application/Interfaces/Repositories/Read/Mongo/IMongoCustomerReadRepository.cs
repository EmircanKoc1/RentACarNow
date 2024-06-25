using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.Application.Interfaces.Repositories.Read.Mongo
{
    public interface IMongoCustomerReadRepository : IMongoReadRepository<Customer>
    {
    }
  

}
