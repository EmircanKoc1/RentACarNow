using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Read.EfCore
{
    public class EfCoreBrandReadRepository : EfCoreBaseReadRepository<Brand>, IEfCoreBrandReadRepository
    {
        public EfCoreBrandReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
