using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Base;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser
{
    public class ClaimAddUserCommandResponse : BaseCommandResponse
    {
        public Guid UserId { get; set; }
        public Guid ClaimId { get; set; }
      
    }

}
