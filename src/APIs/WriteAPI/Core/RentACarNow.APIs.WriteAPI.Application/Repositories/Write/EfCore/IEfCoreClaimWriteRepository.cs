using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore
{
    public interface IEfCoreClaimWriteRepository : IEfWriteRepository<Claim>
    {
        Task AddClaimToAdmin(Guid claimId , Guid adminId);
        Task AddClaimToEmployee(Guid claimId , Guid employeeId);
        Task AddClaimToCustomer(Guid claimId , Guid customerId);


    }
}
