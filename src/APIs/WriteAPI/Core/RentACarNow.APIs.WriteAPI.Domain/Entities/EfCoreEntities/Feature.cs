namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Feature : Common.Concrete.EFBaseEntity, Common.Interfaces.IEFEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
