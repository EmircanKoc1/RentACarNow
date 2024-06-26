using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Write.EfCore
{
    public class EfCoreFeatureWriteRepository : EfCoreBaseWriteRepository<Feature>, IEfCoreFeatureWriteRepository
    {
        public EfCoreFeatureWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
