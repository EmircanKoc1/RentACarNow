using MongoDB.Driver;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.MongoContexts.Interfaces
{
    public interface IMongoDBContext
    {
        IMongoCollection<Admin> AdminCollection { get; }
        IMongoCollection<Customer> CustomerCollection { get; }
        IMongoCollection<Car> CarCollection { get; }
        IMongoCollection<Brand> BrandCollection { get; }
        IMongoCollection<Rental> RentalCollection { get; }
        IMongoCollection<Employee> EmployeeCollection { get; }
        IMongoCollection<T> GetCollection<T>() where T : IMongoEntity;

    }
}
