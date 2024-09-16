using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class CarMapProfile : Profile
    {
        public CarMapProfile()
        {
            CreateMap<Car, GetAllCarQueryResponse>()
                .ForMember(dest => dest.CarId, src => src.MapFrom(c => c.Id));

            CreateMap<Car, GetByIdCarQueryResponse>()
                .ForMember(dest => dest.CarId, src => src.MapFrom(c => c.Id)); 


            //CreateMap<List<Car>, IEnumerable<GetAllAdminQueryResponse>>();

            CreateMap<CarDTO, Car>()
                .ReverseMap();



        }

    }
}
