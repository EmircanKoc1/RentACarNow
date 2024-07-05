using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Brand : EFBaseEntity, IEFEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Car> Cars { get; set; }

    }
}
