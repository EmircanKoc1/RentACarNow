using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoBrandWriteRepository : MongoBaseWriteRepository<Brand>, IMongoBrandWriteRepository
    {
        public MongoBrandWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public override async Task UpdateAsync(Brand entity)
        {
            var filterDefination = Builders<Brand>.Filter.Eq(f => f.Id, entity.Id);

            var updateDefination = Builders<Brand>.Update
                .Set(f => f.UpdatedDate, DateTime.Now)
                .Set(f => f.Name, entity.Name)
                .Set(f => f.Description, entity.Description);

            await _collection.UpdateOneAsync(filterDefination, updateDefination);
        }

        public async Task UpdateCarInBrand(Guid brandId, Guid carId, Car entity)
        {
            var filterDefinition = Builders<Brand>.Filter.And(
                Builders<Brand>.Filter.Eq(brand => brand.Id, brandId),
                Builders<Brand>.Filter.ElemMatch(brand => brand.Cars, car => car.Id == carId)
                );

            var updateDefinition = Builders<Brand>.Update
                .Set("Cars.$", entity);

            await _collection.UpdateOneAsync(filterDefinition, updateDefinition);

        }

    }


}
