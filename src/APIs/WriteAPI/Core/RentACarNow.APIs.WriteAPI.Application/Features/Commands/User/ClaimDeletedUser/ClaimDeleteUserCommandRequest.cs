using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimDeletedUser
{
    public class ClaimDeleteUserCommandRequest : IRequest<ClaimDeleteUserCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid ClaimId { get; set; }
    }

}
