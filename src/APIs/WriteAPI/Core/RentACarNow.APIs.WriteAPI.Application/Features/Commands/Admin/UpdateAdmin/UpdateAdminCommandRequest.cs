using MediatR;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.UpdateAdmin
{
    public class UpdateAdminCommandRequest : IRequest<UpdateAdminCommandResponse>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<CreateClaimCommandRequest> Claims { get; set; }

    }

}
