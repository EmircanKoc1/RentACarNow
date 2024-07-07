using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo
{
    public interface IMongoClaimWriteRepository : IMongoWriteRepository<Claim>
    {
        Task AddClaimToAdminAsync(Claim claim, Guid adminId);
        Task AddClaimToEmployeeAsync(Claim claim, Guid employeeId);
        Task AddClaimToCustomerAsync(Claim claim, Guid customerId);


    }

}
