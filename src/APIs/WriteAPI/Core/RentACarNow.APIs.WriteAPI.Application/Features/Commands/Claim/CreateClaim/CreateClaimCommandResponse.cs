using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Base;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandResponse : BaseCommandResponse
    {
        public Guid ClaimId { get; set; }

    }

}
