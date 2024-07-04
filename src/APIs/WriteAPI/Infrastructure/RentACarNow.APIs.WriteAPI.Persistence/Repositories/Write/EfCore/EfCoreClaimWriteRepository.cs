using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreClaimWriteRepository : EfCoreBaseWriteRepository<Claim>, IEfCoreClaimWriteRepository
    {
        public EfCoreClaimWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }



}
