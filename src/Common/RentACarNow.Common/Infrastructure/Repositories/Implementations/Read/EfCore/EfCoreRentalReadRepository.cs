using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Read.EfCore
{
    public class EfCoreRentalReadRepository : EfCoreBaseReadRepository<Rental>, IEfCoreRentalReadRepository
    {
        public EfCoreRentalReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
