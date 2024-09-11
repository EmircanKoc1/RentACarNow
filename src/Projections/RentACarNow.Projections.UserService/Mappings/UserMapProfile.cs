using AutoMapper;
using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Events.User;
using System.Security.Claims;

namespace RentACarNow.Projections.UserService.Mappings
{
    internal class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserCreatedEvent, User>();
            CreateMap<UserDeletedEvent, User>();
            CreateMap<UserUpdatedEvent, User>();
            CreateMap<UserClaimDeletedEvent, Claim>();
            CreateMap<UserClaimUpdatedEvent, Claim>();
            CreateMap<UserClaimUpdatedEvent, Claim>();
        }

    }
}
