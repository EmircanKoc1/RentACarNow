using MongoDB.Driver;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoCustomerWriteRepository : MongoBaseWriteRepository<Customer>, IMongoCustomerWriteRepository
    {
        public MongoCustomerWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public override async Task UpdateAsync(Customer entity)
        {
            var filterDefination = Builders<Customer>.Filter.Eq(f => f.Id, entity.Id);
            var updateDefination = Builders<Customer>.Update
                .Set(f => f.UpdatedDate, DateTime.Now)
                .Set(f => f.Name, entity.Name)
                .Set(f => f.Username, entity.Username)
                .Set(f => f.Surname, entity.Surname)
                .Set(f => f.PhoneNumber, entity.PhoneNumber)
                .Set(f => f.Age, entity.Age)
                .Set(f => f.Email, entity.Email)
                .Set(f => f.WalletBalance, entity.WalletBalance);

            await _collection.UpdateOneAsync(filterDefination, updateDefination);

        }
    }
}
