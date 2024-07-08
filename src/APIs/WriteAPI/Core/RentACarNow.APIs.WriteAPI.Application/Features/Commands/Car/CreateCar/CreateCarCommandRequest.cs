using MediatR;
using RentACarNow.APIs.WriteAPI.Application.DTOs;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.CreateFeature;
using RentACarNow.Common.Enums.EntityEnums;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar
{
    public class CreateCarCommandRequest : IRequest<CreateCarCommandResponse>
    {

        public string Name { get; set; }
        public string Modal { get; set; }
        public string Title { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public float Kilometer { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int PassengerCapacity { get; set; }
        public int LuggageCapacity { get; set; }
        public float FuelConsumption { get; set; }
        public FuelType CarFuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public Guid BrandId { get; set; }
        //public BrandDTO Brand { get; set; }
        public ICollection<FeatureDTO> Features { get; set; }



    }
}
