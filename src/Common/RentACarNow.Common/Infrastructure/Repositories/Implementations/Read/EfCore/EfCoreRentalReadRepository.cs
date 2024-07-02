using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.EfCore
{
    public class EfCoreRentalReadRepository : EfCoreBaseReadRepository<Rental>, IEfCoreRentalReadRepository
    {
        public EfCoreRentalReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
