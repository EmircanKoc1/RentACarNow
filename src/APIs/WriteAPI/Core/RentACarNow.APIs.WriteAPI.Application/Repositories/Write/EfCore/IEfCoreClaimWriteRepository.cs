using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore
{
    public interface IEfCoreClaimWriteRepository : IEfWriteRepository<Claim>
    {
        Task AddClaimToAdminAsync(Guid claimId , Guid adminId);
        Task AddClaimToEmployeeAsync(Guid claimId , Guid employeeId);
        Task AddClaimToCustomerAsync(Guid claimId , Guid customerId);


    }
}
