using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Feature :EFBaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
