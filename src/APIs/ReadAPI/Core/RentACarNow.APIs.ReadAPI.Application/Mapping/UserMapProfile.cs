using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetById;
using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserMessage, User>()
                .ForMember(u => u.Id, src => src.MapFrom(um => um.UserId));

            CreateMap<User, UserDTO>()
                .ForMember(u => u.Id, src => src.MapFrom(ud => ud.Id));


            CreateMap<User,GetAllUserQueryResponse>()
                .ForMember(u => u.UserId, src => src.MapFrom(ud => ud.Id));

            CreateMap<User, GetByIdUserQueryResponse>()
               .ForMember(u => u.UserId, src => src.MapFrom(ud => ud.Id));



        }
    }
}
