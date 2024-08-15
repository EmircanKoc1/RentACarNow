using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser
{
    public class UserCreateCommandCommandHandler : IRequestHandler<UserCreateCommandRequest, UserCreateCommandResponse>
    {
        public Task<UserCreateCommandResponse> Handle(UserCreateCommandRequest request, CancellationToken cancellationToken)
        {


            return null;
        }
    }

}
