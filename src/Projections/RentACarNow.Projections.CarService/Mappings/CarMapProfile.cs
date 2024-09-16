using AutoMapper;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Projections.CarService.Mappings
{
    internal class CarMapProfile : Profile
    {
        public CarMapProfile()
        {
            CreateMap<CarCreatedEvent, Car>()
                .ForMember(dest => dest.Id, src => src.MapFrom(cce => cce.CarId));
            CreateMap<CarDeletedEvent, Car>()
                .ForMember(dest => dest.Id, src => src.MapFrom(cde => cde.CarId));

            CreateMap<CarUpdatedEvent, Car>()
                .ForMember(dest => dest.Id, src => src.MapFrom(cue => cue.CarId));

            CreateMap<BrandMessage,Brand>()
                .ForMember(dest => dest.Id, src => src.MapFrom(bce => bce.BrandId));

        }
    }
}
