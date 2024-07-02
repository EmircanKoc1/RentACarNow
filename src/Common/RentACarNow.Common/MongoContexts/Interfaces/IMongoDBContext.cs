using MongoDB.Driver;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.Common.MongoContexts.Interfaces
{
    public interface IMongoDBContext
    {
        IMongoCollection<Admin> AdminCollection { get; }
        IMongoCollection<Customer> CustomerCollection { get; }
        IMongoCollection<Car> CarCollection { get; }
        IMongoCollection<Brand> BrandCollection { get; }
        IMongoCollection<Rental> RentalCollection { get; }
        IMongoCollection<T> GetCollection<T>() where T : IMongoEntity;

    }
}
