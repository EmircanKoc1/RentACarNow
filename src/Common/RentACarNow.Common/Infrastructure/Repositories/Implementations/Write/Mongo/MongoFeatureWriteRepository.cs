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

        public override Task UpdateAsync(Feature entity)
        {
            throw new NotImplementedException();
        }
    }

}
