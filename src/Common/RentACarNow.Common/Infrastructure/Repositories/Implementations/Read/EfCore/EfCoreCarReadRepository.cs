using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.EfCore
{
    public class EfCoreCarReadRepository : EfCoreBaseReadRepository<Car>, IEfCoreCarReadRepository
    {
        public EfCoreCarReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
