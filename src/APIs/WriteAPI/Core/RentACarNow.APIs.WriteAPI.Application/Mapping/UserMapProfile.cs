using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.DeleteUser;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.UpdateUser;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.User;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {

            CreateMap<User, UserCreatedEvent>();
            CreateMap<User, UserDeletedEvent>();
            CreateMap<User, UserUpdatedEvent>();



            CreateMap<CreateUserCommandRequest, User>();


            CreateMap<UpdateUserCommandRequest, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.UserId));

            CreateMap<DeleteUserCommandRequest, User>()
                 .ForMember(dest => dest.Id, src => src.MapFrom(p => p.UserId));


            CreateMap<User, UserMessage>()
                .ReverseMap();

        }

    }
}
