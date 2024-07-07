using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoClaimWriteRepository : MongoBaseWriteRepository<Claim>, IMongoClaimWriteRepository
    {
        public MongoClaimWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public async Task AddClaimToAdminAsync(Claim claim, Guid adminId)
        {
            var filterDefinition = Builders<Admin>.Filter.Eq(a => a.Id, adminId);
            var updateDefinition = Builders<Admin>.Update.Push(a => a.Claims, claim);

            var result = await _context.AdminCollection.UpdateOneAsync(filterDefinition, updateDefinition);

        }

        public async Task AddClaimToCustomerAsync(Claim claim, Guid customerId)
        {
            var filterDefinition = Builders<Customer>.Filter.Eq(a => a.Id, customerId);

            var updateDefinition = Builders<Customer>.Update.Push(a => a.Claims, claim);

            await _context.CustomerCollection.UpdateOneAsync(filterDefinition, updateDefinition);
        }

        public async Task AddClaimToEmployeeAsync(Claim claim, Guid employeeId)
        {
            var filterDefinition = Builders<Employee>.Filter.Eq(a => a.Id, employeeId);

            var updateDefinition = Builders<Employee>.Update.Push(a => a.Claims, claim);

            await _context.EmployeeCollection.UpdateOneAsync(filterDefinition, updateDefinition);
        }

        public override Task UpdateAsync(Claim entity)
        {
            throw new NotImplementedException();
        }
    }

}
