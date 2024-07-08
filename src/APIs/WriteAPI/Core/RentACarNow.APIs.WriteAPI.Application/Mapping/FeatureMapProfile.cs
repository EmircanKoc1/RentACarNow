using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.DTOs;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.CreateFeature;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.DeleteFeature;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.UpdateFeature;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Feature;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class FeatureMapProfile : Profile
    {
        public FeatureMapProfile()
        {
            CreateMap<CreateFeatureCommandRequest, Feature>();
            CreateMap<DeleteFeatureCommandRequest, Feature>();
            CreateMap<UpdateFeatureCommandRequest, Feature>();

            CreateMap<Feature, FeatureAddedEvent>();
            CreateMap<Feature, FeatureDeletedEvent>();
            CreateMap<Feature, FeatureUpdatedEvent>();

            CreateMap<FeatureDTO, Feature>();

        }


    }
}
