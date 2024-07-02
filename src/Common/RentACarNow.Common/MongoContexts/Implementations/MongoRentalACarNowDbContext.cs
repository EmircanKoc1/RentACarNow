using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RentACarNow.Common.MongoContexts.Interfaces;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Common.Settings;

namespace RentACarNow.Common.MongoContexts.Implementations
{
    public class MongoRentalACarNowDbContext : IMongoDBContext
    {
        protected readonly IMongoDatabase _database;

        public MongoRentalACarNowDbContext(IConfiguration configuration)
        {
            IMongoClient client = new MongoClient(configuration["MongoDb:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDb:DatabaseName"]);
        }

        public MongoRentalACarNowDbContext(IOptions<MongoDbSettings> settings)
        {
            if (settings is null)
                throw new Exception();

            IMongoClient client = new MongoClient(connectionString: settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);

        }

        public IMongoCollection<Admin> AdminCollection => GetCollection<Admin>();
        public IMongoCollection<Customer> CustomerCollection => GetCollection<Customer>();
        public IMongoCollection<Car> CarCollection => GetCollection<Car>();
        public IMongoCollection<Brand> BrandCollection => GetCollection<Brand>();
        public IMongoCollection<Rental> RentalCollection => GetCollection<Rental>();

        public IMongoCollection<T> GetCollection<T>() where T : IMongoEntity
            => _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());


    }
}
