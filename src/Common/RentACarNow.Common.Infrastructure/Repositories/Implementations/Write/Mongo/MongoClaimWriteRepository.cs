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

        public override async Task UpdateAsync(Claim entity)
        {
            var filterDefinition = Builders<Claim>.Filter.Eq(c => c.Id, entity.Id);
            await _collection.ReplaceOneAsync(filterDefinition,entity);
        }
    }

}
