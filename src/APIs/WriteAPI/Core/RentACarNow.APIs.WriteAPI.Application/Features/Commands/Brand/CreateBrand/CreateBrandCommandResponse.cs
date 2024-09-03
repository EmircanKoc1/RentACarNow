using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandResponse
    {
        public Guid BrandId { get; set; }
        public HttpStatusCode StatusCode { get; set; } 
        public IEnumerable<ResponseErrorModel>? Errors { get; set; }
    }

}
