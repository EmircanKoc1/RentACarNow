using AutoMapper;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.BrandService.Mappings
{
    public class BrandMapProfile : Profile
    {
        public BrandMapProfile()
        {
            CreateMap<BrandCreatedEvent, Brand>();
            CreateMap<BrandDeletedEvent, Brand>();
            CreateMap<BrandUpdatedEvent, Brand>();
        }

    }
}
