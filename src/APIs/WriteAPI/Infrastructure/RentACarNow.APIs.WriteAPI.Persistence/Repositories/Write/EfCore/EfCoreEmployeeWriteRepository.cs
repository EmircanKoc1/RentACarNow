using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreEmployeeWriteRepository : EfCoreBaseWriteRepository<Employee>, IEfCoreEmployeeWriteRepository
    {
        public EfCoreEmployeeWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
