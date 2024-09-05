using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureUpdateCar
{
    public class FeatureUpdateCarCommandResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<ResponseErrorModel>? Errors { get; set; }

    }
}
