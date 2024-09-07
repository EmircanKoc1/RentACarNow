using AutoMapper;
using MongoDB.Driver;
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
            CreateMap<CreateRentalCommandRequest, Rental>()
                .ForMember(dest => dest.CarId, src => src.MapFrom(p => p.CarId))
                .ForMember(dest => dest.UserId,src=>src.MapFrom(p=>p.UserId));



            CreateMap<DeleteRentalCommandRequest, Rental>()
                .ForMember(dest=>dest.Id, src=>src.MapFrom(f=>f.RentalId));


            CreateMap<UpdateRentalCommandRequest, Rental>()
                .ForMember(dest => dest.Id, src => src.MapFrom(f => f.RentalId));



            CreateMap<CreateRentalCommandRequest, Rental>();

            CreateMap<Rental, RentalCreatedEvent>();
            CreateMap<Rental, RentalDeletedEvent>();
            CreateMap<Rental, RentalUpdatedEvent>();

        }



    }
}
