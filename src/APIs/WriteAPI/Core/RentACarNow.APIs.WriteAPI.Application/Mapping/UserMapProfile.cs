using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.DeleteUser;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.UpdateUser;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<UpdateUserCommandRequest, User>();
            CreateMap<DeleteUserCommandRequest, User>();

            CreateMap<User, UserMessage>()
                .ReverseMap();

        }

    }
}
