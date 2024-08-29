using MongoDB.Driver;
using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.MongoContexts.Interfaces
{
    public interface IMongoDBContext
    {

        IMongoCollection<Car> CarCollection { get; }
        IMongoCollection<Brand> BrandCollection { get; }
        IMongoCollection<Rental> RentalCollection { get; }
        IMongoCollection<User> UserCollection { get; }
        IMongoCollection<Claim> ClaimCollection { get; }
        IMongoCollection<T> GetCollection<T>() where T : IMongoEntity;

    }
}
