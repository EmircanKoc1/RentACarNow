using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore
{
    public interface IEfCoreClaimWriteRepository : IEfWriteRepository<Claim>
    {
        Task<Claim> AddClaimToAdminAsync(Guid claimId, Guid adminId);
        Task<Claim> AddClaimToEmployeeAsync(Guid claimId, Guid employeeId);
        Task<Claim> AddClaimToCustomerAsync(Guid claimId, Guid customerId);

        Task<bool> DeleteClaimFromAdminAsync(Guid claimId, Guid adminId);
        Task<bool> DeleteClaimFromCustomerAsync(Guid claimId, Guid customerId);
        Task<bool> DeleteClaimFromEmployeeAsync(Guid claimId, Guid employeeId);
    }
}
