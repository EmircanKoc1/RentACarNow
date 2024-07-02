using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.EfCore
{
    public class EfCoreEmployeeReadRepository : EfCoreBaseReadRepository<Employee>, IEfCoreEmployeeReadRepository
    {
        public EfCoreEmployeeReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
