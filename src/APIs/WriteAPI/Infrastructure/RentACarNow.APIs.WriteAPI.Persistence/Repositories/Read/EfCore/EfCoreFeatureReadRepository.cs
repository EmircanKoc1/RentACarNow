using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore
{
    public class EfCoreFeatureReadRepository : EfCoreBaseReadRepository<Feature>, IEfCoreFeatureReadRepository
    {
        public EfCoreFeatureReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
