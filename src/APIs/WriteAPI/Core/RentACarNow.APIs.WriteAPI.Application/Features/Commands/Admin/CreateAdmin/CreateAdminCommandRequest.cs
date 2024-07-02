using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequest : IRequest<CreateAdminCommandResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Guid ClaimId { get; set; }
    }

}
