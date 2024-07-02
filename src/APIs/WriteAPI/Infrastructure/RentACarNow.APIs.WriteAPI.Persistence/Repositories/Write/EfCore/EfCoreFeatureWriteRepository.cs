using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreFeatureWriteRepository : EfCoreBaseWriteRepository<Feature>, IEfCoreFeatureWriteRepository
    {
        public EfCoreFeatureWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
