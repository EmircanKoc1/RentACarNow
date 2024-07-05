using MediatR;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequest : IRequest<CreateAdminCommandResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<CreateClaimCommandRequest> Claims { get; set; }
    }

}
