using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.DTOs;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Feature;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class FeatureMapProfile : Profile
    {
        public FeatureMapProfile()
        {

            CreateMap<Feature, FeatureAddedCarEvent>();
            CreateMap<Feature, FeatureDeletedEvent>();
            CreateMap<Feature, FeatureUpdatedEvent>();

            CreateMap<FeatureDTO, Feature>();

        }


    }
}
