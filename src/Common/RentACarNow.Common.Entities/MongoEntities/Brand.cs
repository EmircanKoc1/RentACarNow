using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.MongoEntities
{
    public class Brand : MongoBaseEntity, IMongoEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //public ICollection<Car> Cars { get; set; }

    }
}
