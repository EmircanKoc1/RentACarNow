using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo
{
    public interface IMongoUserWriteRepository : IMongoWriteRepository<User>
    {

        Task DeleteUserClaimAsync(Guid userId , Guid claimId);
        Task UpdateUserClaimAsync(Claim entity);
        Task AddUserClaimAsync(Guid userId , Claim entity);


    }

}
