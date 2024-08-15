using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo
{
    public interface IMongoFeatureWriteRepository : IMongoWriteRepository<Feature>
    {

        Task AddFeatureByCarId(Guid carId, Feature entity);

        Task UpdateAsyncByCarId(Guid carId, Feature entity);

        Task DeleteAsyncByCarId(Guid carId, Guid featureId);

    }

}
