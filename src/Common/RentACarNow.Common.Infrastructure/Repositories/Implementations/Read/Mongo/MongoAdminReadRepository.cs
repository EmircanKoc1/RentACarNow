using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo
{

    public class MongoAdminReadRepository : MongoBaseReadRepository<Admin>, IMongoAdminReadRepository
    {
        public MongoAdminReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public async Task<Admin> GetAdminByLogin(string usernameOrEmail, string password)
        {
            var filterDefinition = Builders<Admin>.Filter.And(
                 Builders<Admin>.Filter.Eq(x => x.Password, password),
                 Builders<Admin>.Filter.Or(
                     Builders<Admin>.Filter.Eq(x => x.Username, usernameOrEmail),
                     Builders<Admin>.Filter.Eq(x => x.Email, usernameOrEmail)));

            var user = await(await _collection.FindAsync(filterDefinition)).FirstOrDefaultAsync();

            return user;
        }
    }


}
