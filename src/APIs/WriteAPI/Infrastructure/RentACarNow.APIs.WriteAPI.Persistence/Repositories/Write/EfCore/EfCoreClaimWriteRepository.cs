using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreClaimWriteRepository : EfCoreBaseWriteRepository<Claim>,  IEfCoreClaimWriteRepository
    {
        public EfCoreClaimWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }

        public Task AddClaimToAdmin(Guid claimId, Guid adminId)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimToCustomer(Guid claimId, Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimToEmployee(Guid claimId, Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }



}
