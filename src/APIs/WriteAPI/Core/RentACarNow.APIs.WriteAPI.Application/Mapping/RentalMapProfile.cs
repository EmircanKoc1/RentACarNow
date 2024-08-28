using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.UpdateRental;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Rental;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class RentalMapProfile : Profile
    {
        public RentalMapProfile()
        {
            CreateMap<CreateRentalCommandRequest, Rental>();
            CreateMap<DeleteRentalCommandRequest, Rental>();
            CreateMap<UpdateRentalCommandRequest, Rental>();

            CreateMap<Rental, RentalCreatedEvent>();
            CreateMap<Rental, RentalDeletedEvent>();
            CreateMap<Rental, RentalUpdatedEvent>();

        }



    }
}
