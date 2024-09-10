using AutoMapper;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.RentalService.Mappings
{
    internal class RentalMapProfile : Profile
    {

        public RentalMapProfile()
        {
            CreateMap<RentalCreatedEvent, Rental>();
            CreateMap<RentalUpdatedEvent, Rental>();
            CreateMap<RentalDeletedEvent, Rental>();


        }

    }
}
