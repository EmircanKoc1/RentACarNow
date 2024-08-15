using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser
{
    public class ClaimAddUserCommandRequest : IRequest<ClaimAddUserCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid ClaimId { get; set; }
    }

}
