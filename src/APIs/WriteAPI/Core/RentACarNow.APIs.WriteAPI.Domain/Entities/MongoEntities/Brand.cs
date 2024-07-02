using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.MongoEntities
{
    public class Brand : BaseEntity, IMongoEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Car> Cars { get; set; }

    }
}
