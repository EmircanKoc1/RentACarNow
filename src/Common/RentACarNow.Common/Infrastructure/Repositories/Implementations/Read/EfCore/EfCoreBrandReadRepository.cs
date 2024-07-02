using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.EfCore
{
    public class EfCoreBrandReadRepository : EfCoreBaseReadRepository<Brand>, IEfCoreBrandReadRepository
    {
        public EfCoreBrandReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
