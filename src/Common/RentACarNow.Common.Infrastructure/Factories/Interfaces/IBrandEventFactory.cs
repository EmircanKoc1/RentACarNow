using RentACarNow.Common.Events.Brand;

namespace RentACarNow.Common.Infrastructure.Factories.Interfaces
{
    public interface IBrandEventFactory
    {
        BrandCreatedEvent CreateBrandCreatedEvent(Guid brandId, string name, string description, DateTime createdDate);
        BrandDeletedEvent CreateBrandDeletedEvent(Guid brandId, DateTime deletedDate);
        BrandUpdatedEvent CreateBrandUpdatedEvent(Guid brandId, string name, string description, DateTime updatedDate);
    }
}
