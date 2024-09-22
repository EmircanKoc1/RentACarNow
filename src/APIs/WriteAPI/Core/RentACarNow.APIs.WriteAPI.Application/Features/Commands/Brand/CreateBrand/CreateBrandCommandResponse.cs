using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Base;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandResponse : BaseCommandResponse
    {
        public Guid BrandId { get; set; }
    }

}
