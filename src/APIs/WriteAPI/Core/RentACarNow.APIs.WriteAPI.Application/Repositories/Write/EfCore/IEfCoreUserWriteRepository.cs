using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore
{
    public interface IEfCoreUserWriteRepository : IEfWriteRepository<User>
    {
        Task AddClaimToUser(Guid userId , Guid claimId);
        Task DeleteClaimToUser(Guid userId , Guid claimId);
    }
}
