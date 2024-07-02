using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreCustomerWriteRepository : EfCoreBaseWriteRepository<Customer>, IEfCoreCustomerWriteRepository
    {
        public EfCoreCustomerWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
