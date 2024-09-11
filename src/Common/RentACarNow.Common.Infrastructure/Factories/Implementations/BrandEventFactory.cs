using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Infrastructure.Factories.Implementations
{
    public class BrandEventFactory : IBrandEventFactory
    {
        public BrandCreatedEvent CreateBrandCreatedEvent(Guid brandId, string name, string description, DateTime createdDate)
        {
            return new BrandCreatedEvent(brandId, name, description, createdDate);
        }

        public BrandDeletedEvent CreateBrandDeletedEvent(Guid brandId, DateTime deletedDate)
        {
            return new BrandDeletedEvent(brandId, deletedDate);
        }

        public BrandUpdatedEvent CreateBrandUpdatedEvent(Guid brandId, string name, string description, DateTime updatedDate)
        {
            return new BrandUpdatedEvent(brandId, name, description, updatedDate);
        }
    }
}
