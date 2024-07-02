using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Brand : Common.Concrete.EFBaseEntity, Common.Interfaces.IEFEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        ICollection<Car> Cars { get; set; }

    }
}
