using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetById;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class FeatureMapProfile : Profile
    {
        public FeatureMapProfile()
        {
            CreateMap<FeatureDTO, Feature>()
                .ReverseMap();

            CreateMap<Feature, GetAllFeatureQueryResponse>();
            CreateMap<Feature, GetByIdFeatureQueryResponse>();


        }

    }
}
