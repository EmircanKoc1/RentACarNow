using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoAdminWriteRepository : MongoBaseWriteRepository<Admin>, IMongoAdminWriteRepository
    {
        public MongoAdminWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public override async Task UpdateAsync(Admin entity)
        {

            var filterDefination = Builders<Admin>.Filter.Eq(admin => admin.Id, entity.Id);
            var updateDefination = Builders<Admin>.Update
                .Set(f => f.UpdatedDate, DateTime.Now)
                .Set(f => f.Email, entity.Email)
                .Set(f => f.Password, entity.Password)
                .Set(f => f.Username, entity.Username);

            await _context.AdminCollection.UpdateOneAsync(filterDefination, updateDefination);


        }
    }
}
