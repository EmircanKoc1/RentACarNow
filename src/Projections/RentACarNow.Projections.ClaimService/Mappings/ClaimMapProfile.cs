using AutoMapper;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.ClaimService.Mappings
{
    internal class ClaimMapProfile : Profile
    {

        public ClaimMapProfile()
        {
            CreateMap<ClaimAddedEvent, Claim>();
            CreateMap<ClaimUpdatedEvent, Claim>();
            CreateMap<ClaimDeletedEvent, Claim>();
        }

    }
}
