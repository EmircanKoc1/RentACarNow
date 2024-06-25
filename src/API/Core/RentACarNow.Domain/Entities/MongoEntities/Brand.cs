using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.Domain.Entities.MongoEntities
{
    public class Brand : BaseEntity,IMongoEntity
    {
        public string Name{ get; set; }
        public string Description{ get; set; }

        ICollection<Car> Cars { get; set; } 

    }
}
