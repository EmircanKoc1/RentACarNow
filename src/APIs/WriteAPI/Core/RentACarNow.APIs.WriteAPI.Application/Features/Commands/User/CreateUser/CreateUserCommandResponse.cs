using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Base;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser
{

    public class CreateUserCommandResponse : BaseCommandResponse
    {
        public Guid UserId { get; set; }
       
    }

}
