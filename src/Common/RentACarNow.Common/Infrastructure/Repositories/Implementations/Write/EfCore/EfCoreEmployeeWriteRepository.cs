using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.EfCore
{
    public class EfCoreEmployeeWriteRepository : EfCoreBaseWriteRepository<Employee>, IEfCoreEmployeeWriteRepository
    {
        public EfCoreEmployeeWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
