using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.Application.Interfaces.Repositories.Write.EfCore
{
    public interface IEfCoreFeatureWriteRepository : IEfWriteRepository<Feature>
    {
    }
}
