using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class RentalMapProfile : Profile
    {
        public RentalMapProfile()
        {
            CreateMap<Rental, GetByIdRentalQueryResponse>()
                .ForMember(dest=>dest.RentalId,src=>src.MapFrom(r=>r.Id));
            CreateMap<Rental, GetAllRentalQueryResponse>()
                .ForMember(dest => dest.RentalId, src => src.MapFrom(r => r.Id));



        }

    }
}
