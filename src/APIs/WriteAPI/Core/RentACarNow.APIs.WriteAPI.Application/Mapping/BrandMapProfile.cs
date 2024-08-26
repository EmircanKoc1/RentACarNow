using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class BrandMapProfile : Profile
    {

        public BrandMapProfile()
        {
            CreateMap<Brand, BrandMessage>().ReverseMap();

            CreateMap<CreateBrandCommandRequest, Brand>();
            CreateMap<DeleteBrandCommandRequest, Brand>();
            CreateMap<UpdateBrandCommandRequest, Brand>();

            CreateMap<Brand, BrandCreatedEvent>();
            CreateMap<Brand, BrandDeletedEvent>();
            CreateMap<Brand, BrandUpdatedEvent>();

        }

    }
}
