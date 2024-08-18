using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Brand : EFBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();

    }
}
