using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<ResponseErrorModel>? Errors { get; set; }
    }

}
