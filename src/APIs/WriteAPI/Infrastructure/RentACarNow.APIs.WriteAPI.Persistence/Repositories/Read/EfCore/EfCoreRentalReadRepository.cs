using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore
{
    public class EfCoreRentalReadRepository : EfCoreBaseReadRepository<Rental>, IEfCoreRentalReadRepository
    {
        public EfCoreRentalReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
