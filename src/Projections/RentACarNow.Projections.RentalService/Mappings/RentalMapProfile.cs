using AutoMapper;
using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.RentalService.Mappings
{
    internal class RentalMapProfile : Profile
    {

        public RentalMapProfile()
        {
            CreateMap<RentalCreatedEvent, Rental>()
                .ForMember(dest => dest.Id, src => src.MapFrom(r => r.RentalId));
            CreateMap<RentalUpdatedEvent, Rental>()
                .ForMember(dest => dest.Id, src => src.MapFrom(r => r.RentalId));
            CreateMap<RentalDeletedEvent, Rental>()
                .ForMember(dest => dest.Id, src => src.MapFrom(r => r.RentalId));

            CreateMap<CarMessage, Car>()
                .ForMember(dest => dest.Id, src => src.MapFrom(cm => cm.CarId));

            CreateMap<UserMessage, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(um => um.UserId));

        }

    }
}
