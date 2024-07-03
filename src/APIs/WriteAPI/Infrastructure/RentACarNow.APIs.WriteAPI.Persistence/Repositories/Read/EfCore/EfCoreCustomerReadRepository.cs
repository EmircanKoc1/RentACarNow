using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore
{
    public class EfCoreCustomerReadRepository : EfCoreBaseReadRepository<Customer>, IEfCoreCustomerReadRepository
    {
        public EfCoreCustomerReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
