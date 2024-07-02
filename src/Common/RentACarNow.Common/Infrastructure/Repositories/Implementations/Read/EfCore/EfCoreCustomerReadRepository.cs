using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using RentACarNow.Persistence.Repositories.Base;

namespace RentACarNow.Persistence.Repositories.Read.EfCore
{
    public class EfCoreCustomerReadRepository : EfCoreBaseReadRepository<Customer>, IEfCoreCustomerReadRepository
    {
        public EfCoreCustomerReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
