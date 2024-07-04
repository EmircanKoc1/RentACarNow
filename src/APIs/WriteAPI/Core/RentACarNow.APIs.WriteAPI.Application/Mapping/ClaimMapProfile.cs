using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaim;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.UpdateClaim;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Claim;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class ClaimMapProfile : Profile
    {
        public ClaimMapProfile()
        {

            CreateMap<CreateClaimCommandRequest, Claim>();
            CreateMap<DeleteClaimCommandRequest, Claim>();
            CreateMap<UpdateClaimCommandRequest, Claim>();

            CreateMap<Claim, ClaimAddedEvent>();
            CreateMap<Claim, ClaimDeletedEvent>();
            CreateMap<Claim, ClaimUpdatedEvent>();



        }
    }
}
