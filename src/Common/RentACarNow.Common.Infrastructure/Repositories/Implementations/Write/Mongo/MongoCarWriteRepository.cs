using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoCarWriteRepository : MongoBaseWriteRepository<Car>, IMongoCarWriteRepository
    {
        public MongoCarWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public override async Task UpdateAsync(Car entity)
        {
            var filterDefination = Builders<Car>.Filter.Eq(f => f.Id, entity.Id);

            var updateDefination = Builders<Car>.Update
                .Set(f => f.UpdatedDate, DateTime.Now)
                .Set(f => f.Title, entity.Title)
                .Set(f => f.CarFuelType, entity.CarFuelType)
                .Set(f => f.TransmissionType, entity.TransmissionType)
                .Set(f => f.Color, entity.Color)
                .Set(f => f.Modal, entity.Modal)
                .Set(f => f.FuelConsumption, entity.FuelConsumption)
                .Set(f => f.Description, entity.Description)
                .Set(f => f.HourlyRentalPrice, entity.HourlyRentalPrice)
                .Set(f => f.Title, entity.Title)
                .Set(f => f.HourlyRentalPrice, entity.HourlyRentalPrice)
                .Set(f => f.Name, entity.Name)
                .Set(f => f.Kilometer, entity.Kilometer)
                .Set(f => f.LuggageCapacity, entity.LuggageCapacity)
                .Set(f => f.PassengerCapacity, entity.PassengerCapacity);

            await _collection.UpdateOneAsync(filterDefination, updateDefination);
        }
    }
}
