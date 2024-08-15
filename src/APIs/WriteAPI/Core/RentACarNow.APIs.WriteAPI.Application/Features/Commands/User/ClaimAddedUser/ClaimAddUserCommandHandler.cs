using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser
{

    public class ClaimAddUserCommandHandler : IRequestHandler<ClaimAddUserCommandRequest, ClaimAddUserCommandResponse>
    {
        public Task<ClaimAddUserCommandResponse> Handle(ClaimAddUserCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
