using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<ResponseErrorModel>? Errors { get; set; }
    }

}
