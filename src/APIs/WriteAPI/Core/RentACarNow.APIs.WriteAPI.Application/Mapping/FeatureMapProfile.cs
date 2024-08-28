using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.DTOs;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureAddCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureDeleteCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureUpdateCar;
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

            CreateMap<FeatureAddCarCommandRequest, Feature>();
            CreateMap<FeatureUpdateCarCommandRequest, Feature>()
                .ForMember(d => d.Id,
                opt => opt.MapFrom(src => src.FeatureId));

            CreateMap<FeatureDeleteCarCommandRequest, Feature>()
                 .ForMember(d => d.Id,
                opt => opt.MapFrom(src => src.FeatureId));
            ;

            CreateMap<FeatureDTO, Feature>();

        }


    }
}
