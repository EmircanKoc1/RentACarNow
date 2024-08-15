using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo
{
    public class MongoCustomerReadRepository : MongoBaseReadRepository<Customer>, IMongoCustomerReadRepository
    {
        public MongoCustomerReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public async Task<Customer> GetCustomerByLogin(string usernameOrEmail, string password)
        {
            var filterDefinition = Builders<Customer>.Filter.And(
                Builders<Customer>.Filter.Eq(x => x.Password, password),
                Builders<Customer>.Filter.Or(
                    Builders<Customer>.Filter.Eq(x => x.Username, usernameOrEmail),
                    Builders<Customer>.Filter.Eq(x => x.Email, usernameOrEmail)));

            var user = await (await _collection.FindAsync(filterDefinition)).FirstOrDefaultAsync();


            return user;
        }
    }

}
