using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.Application.Interfaces.Repositories.Write.Mongo
{
    public interface IMongoCustomerWriteRepository : IMongoWriteRepository<Admin>
    {
    }

}
