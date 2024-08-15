using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo
{
    public class MongoEmployeeReadRepository : MongoBaseReadRepository<Employee>, IMongoEmployeeReadRepository
    {
        public MongoEmployeeReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public async Task<Employee?> GetEmployeeByLogin(string usernameOrEmail, string password)
        {
            var filterDefinition = Builders<Employee>.Filter.And(
                Builders<Employee>.Filter.Eq(x => x.Password, password),
                Builders<Employee>.Filter.Or(
                    Builders<Employee>.Filter.Eq(x => x.Username, usernameOrEmail),
                    Builders<Employee>.Filter.Eq(x => x.Email, usernameOrEmail))
                );

            var user = await (await _collection.FindAsync(filterDefinition)).FirstOrDefaultAsync();


            return user;


        }
    }


}
