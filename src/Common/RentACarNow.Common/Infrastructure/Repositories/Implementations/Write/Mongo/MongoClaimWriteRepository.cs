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

        public override async Task UpdateAsync(Claim entity)
        {
            var filterDefination = Builders<Claim>.Filter.Eq(c => c.Id, entity.Id);
            //var updateDefinition = Builders<Claim>.Update

            var replaceResult = await _collection.ReplaceOneAsync(filterDefination, entity);

            if (replaceResult.ModifiedCount > 0)
            {
                var adminFilter = Builders<Admin>.Filter.ElemMatch(a => a.Claims, Builders<Claim>.Filter.Eq(c => c.Id, entity.Id));

                var customerFilter = Builders<Customer>.Filter.ElemMatch(c => c.Claims, Builders<Claim>.Filter.Eq(c => c.Id, entity.Id));

                var employeeFilter = Builders<Employee>.Filter.ElemMatch(e => e.Claims, Builders<Claim>.Filter.Eq(c => c.Id, entity.Id));


                var adminUpdateDefination = Builders<Admin>.Update
                    .Set("Claims.$", entity);

                var employeeUpdateDefination = Builders<Employee>.Update
                   .Set("Claims.$", entity);

                var customerUpdateDefination = Builders<Customer>.Update
                   .Set("Claims.$", entity);


                var task1 = _context.AdminCollection.UpdateOneAsync(adminFilter, adminUpdateDefination);
                var task2 = _context.EmployeeCollection.UpdateOneAsync(employeeFilter, employeeUpdateDefination);
                var task3 = _context.CustomerCollection.UpdateOneAsync(customerFilter, customerUpdateDefination);



                await Task.WhenAll(task1, task2, task3);
            }



        }
        public async override Task DeleteByIdAsync(Guid id)
        {
            var filter = Builders<Claim>.Filter.Empty;

            var deleteResult = await _collection.DeleteOneAsync(filter);

            if (deleteResult.DeletedCount > 0)
            {
                var customerFilter = Builders<Customer>.Filter.Empty;
                var employeeFilter = Builders<Employee>.Filter.Empty;
                var adminFilter = Builders<Admin>.Filter.Empty;

                var customerUpdateDefinition = Builders<Customer>.Update.PullFilter(
                   field: c => c.Claims,
                   filter: f => f.Id == id);


                var employeeUpdateDefinition = Builders<Employee>.Update.PullFilter(
                       field: e => e.Claims,
                       filter: f => f.Id == id);

                var adminUpdateDefinition = Builders<Admin>.Update.PullFilter(
                      field: a => a.Claims,
                      filter: f => f.Id == id);


                var task1 = _context.AdminCollection.UpdateManyAsync(adminFilter, adminUpdateDefinition);
                var task2 = _context.EmployeeCollection.UpdateManyAsync(employeeFilter, employeeUpdateDefinition);
                var task3 = _context.CustomerCollection.UpdateManyAsync(customerFilter, customerUpdateDefinition);

                await Task.WhenAll(task1, task2, task3);
            }

        }

        public override Task DeleteAsync(Claim entity)
            => DeleteByIdAsync(entity.Id);




        public async Task DeleteClaimFromAdminAsync(Guid claimId, Guid adminId)
        {
            var filterDefinition = Builders<Admin>.Filter
                 .Eq(c => c.Id, adminId);

            var updateDefinition = Builders<Admin>.Update
                .PullFilter(a => a.Claims, c => c.Id == claimId);

            await _context.AdminCollection.UpdateOneAsync(filterDefinition, updateDefinition);
        }

        public async Task DeleteClaimFromEmployeeAsync(Guid claimId, Guid employeeId)
        {
            var filterDefinition = Builders<Employee>.Filter
             .Eq(c => c.Id, employeeId);

            var updateDefinition = Builders<Employee>.Update
                .PullFilter(a => a.Claims, c => c.Id == claimId);

            await _context.EmployeeCollection.UpdateOneAsync(filterDefinition, updateDefinition);
        }

        public async Task DeleteClaimFromCustomerAsync(Guid claimId, Guid customerId)
        {

            var filterDefinition = Builders<Customer>.Filter
             .Eq(c => c.Id, customerId);

            var updateDefinition = Builders<Customer>.Update
                .PullFilter(a => a.Claims, c => c.Id == claimId);

            await _context.CustomerCollection.UpdateOneAsync(filterDefinition, updateDefinition);
        }
    }

}
