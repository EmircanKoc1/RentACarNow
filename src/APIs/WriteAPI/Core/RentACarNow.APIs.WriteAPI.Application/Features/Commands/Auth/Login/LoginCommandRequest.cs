using Amazon.Runtime.Internal;
using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Auth.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        public string UserType { get; set; }
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }

    }
}
