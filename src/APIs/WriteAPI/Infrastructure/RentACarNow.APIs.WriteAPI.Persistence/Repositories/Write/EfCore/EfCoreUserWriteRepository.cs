using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreUserWriteRepository : EfCoreBaseWriteRepository<User>, IEfCoreUserWriteRepository
    {
        public EfCoreUserWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
