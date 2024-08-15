using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoRentalWriteRepository : MongoBaseWriteRepository<Rental>, IMongoRentalWriteRepository
    {
        public MongoRentalWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public override async Task UpdateAsync(Rental entity)
        {
            var filterDefination = Builders<Rental>.Filter.Eq(admin => admin.Id, entity.Id);
            var updateDefination = Builders<Rental>.Update
                .Set(f => f.UpdatedDate, DateTime.Now)
                .Set(f => f.Status, entity.Status)
                .Set(f => f.RentalStartedDate, entity.RentalStartedDate)
                .Set(f => f.HourlyRentalPrice, entity.HourlyRentalPrice);


            await _context.RentalCollection.UpdateOneAsync(filterDefination, updateDefination);
        }

      

    }
}
