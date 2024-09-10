using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo
{
    public interface IMongoCarWriteRepository : IMongoWriteRepository<Car>
    {

        Task AddFeatureCarAsync(Guid carId, Feature feature);
        Task DeleteFeatureCarAsync(Guid carId, Guid featureId);
        Task UpateFeatureCarAsync(Guid carId, Feature feature);


    }

}
