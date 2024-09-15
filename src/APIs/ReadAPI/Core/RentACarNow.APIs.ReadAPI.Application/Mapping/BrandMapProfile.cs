using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class BrandMapProfile : Profile
    {
        public BrandMapProfile()
        {
            CreateMap<BrandDTO, Brand>()
                .ReverseMap();

            CreateMap<Brand, GetAllBrandQueryResponse>()
                .ForMember(dest => dest.BrandId, src => src.MapFrom(b => b.Id));
            CreateMap<Brand, GetByIdBrandQueryResponse>()
                  .ForMember(dest => dest.BrandId, src => src.MapFrom(b => b.Id)); 


        }

    }
}
