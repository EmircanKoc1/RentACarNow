using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore
{
    public class EfCoreFeatureReadRepository : EfCoreBaseReadRepository<Feature>, IEfCoreFeatureReadRepository
    {
        public EfCoreFeatureReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
