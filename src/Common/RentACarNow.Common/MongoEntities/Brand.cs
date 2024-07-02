using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Domain.Entities.MongoEntities
{
    public class Brand : BaseEntity,IMongoEntity
    {
        public string Name{ get; set; }
        public string Description{ get; set; }

        public ICollection<Car> Cars { get; set; } 

    }
}
