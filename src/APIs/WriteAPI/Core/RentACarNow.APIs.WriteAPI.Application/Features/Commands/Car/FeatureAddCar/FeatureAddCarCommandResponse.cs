using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureAddCar
{

    public class FeatureAddCarCommandResponse
    {
        public Guid CarId { get; set; }
        public Guid FeatureId { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<ResponseErrorModel>? Errors { get; set; }


    }
}
