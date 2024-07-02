using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo
{
    public interface IMongoCustomerWriteRepository : IMongoWriteRepository<Customer>
    {
    }

}
