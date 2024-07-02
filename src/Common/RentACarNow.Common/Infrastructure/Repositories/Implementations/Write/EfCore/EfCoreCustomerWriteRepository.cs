using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.EfCore
{
    public class EfCoreCustomerWriteRepository : EfCoreBaseWriteRepository<Customer>, IEfCoreCustomerWriteRepository
    {
        public EfCoreCustomerWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
