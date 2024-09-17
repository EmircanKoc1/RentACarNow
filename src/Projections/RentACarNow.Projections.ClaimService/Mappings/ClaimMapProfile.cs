using AutoMapper;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.ClaimService.Mappings
{
    internal class ClaimMapProfile : Profile
    {

        public ClaimMapProfile()
        {
            CreateMap<ClaimCreatedEvent, Claim>()
                .ForMember(dest => dest.Id, src => src.MapFrom(e => e.ClaimId));
            CreateMap<ClaimUpdatedEvent, Claim>()
                .ForMember(dest => dest.Id, src => src.MapFrom(e => e.ClaimId));

            CreateMap<ClaimDeletedEvent, Claim>()
                .ForMember(dest => dest.Id, src => src.MapFrom(e => e.ClaimId));

        }

    }
}
