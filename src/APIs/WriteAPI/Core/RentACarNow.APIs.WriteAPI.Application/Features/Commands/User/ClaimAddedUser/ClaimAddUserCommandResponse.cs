using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser
{
    public class ClaimAddUserCommandResponse
    {
        public Guid UserId { get; set; }
        public Guid ClaimId { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<ResponseErrorModel>? Errors { get; set; }
    }

}
