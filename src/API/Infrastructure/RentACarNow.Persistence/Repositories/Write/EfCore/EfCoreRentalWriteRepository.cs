using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Write.EfCore
{
    public class EfCoreRentalWriteRepository : EfCoreBaseWriteRepository<Rental>, IEfCoreRentalWriteRepository
    {
    }
}
