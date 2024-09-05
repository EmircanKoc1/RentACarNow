using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandResponse
    {

        public Guid ClaimId { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<ResponseErrorModel>? Errors { get; set; }

    }

}
