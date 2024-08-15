using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoFeatureWriteRepository : MongoBaseWriteRepository<Feature>, IMongoFeatureWriteRepository
    {
        public MongoFeatureWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public async Task AddFeatureByCarId(Guid carId, Feature entity)
        {
            var filterDefinition = Builders<Car>.Filter.Eq(c => c.Id, carId);

            var updateDefinition = Builders<Car>.Update.Push(c => c.Features, entity);


            await _context.CarCollection.UpdateOneAsync(filterDefinition, updateDefinition);
        }

        public async Task UpdateAsyncByCarId(Guid carId, Feature entity)
        {
            var filterDefinition = Builders<Car>.Filter.And(
                Builders<Car>.Filter.Eq(c => c.Id, carId),
                Builders<Car>.Filter
                .ElemMatch(c => c.Features, Builders<Feature>.Filter.Eq(f => f.Id, entity.Id)));

            var updateDefinition = Builders<Car>.Update.Set("Features.$", entity);



            await _context.CarCollection.UpdateOneAsync(filterDefinition, updateDefinition);
        }


        [Obsolete("Not supported")]
        public override Task UpdateAsync(Feature entity)
        {
            throw new NotSupportedException();
        }

        public async Task DeleteAsyncByCarId(Guid carId, Guid featureId)
        {
            var filterDefinition = Builders<Car>.Filter.Where(car => car.Id == carId);

            var updateDefinition = Builders<Car>.Update.PullFilter(c => c.Features, f => f.Id == featureId);

            await _context.CarCollection.UpdateOneAsync(filterDefinition, updateDefinition);

        }
    }

}
