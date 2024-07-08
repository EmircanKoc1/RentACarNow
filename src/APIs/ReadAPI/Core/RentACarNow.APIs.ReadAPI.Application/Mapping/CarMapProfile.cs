using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById;
using RentACarNow.Common.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class CarMapProfile : Profile
    {
        public CarMapProfile()
        {
            CreateMap<Car, GetAllCarQueryResponse>(); 
            CreateMap<Car, GetByIdCarQueryResponse>();

            CreateMap<CarDTO, Car>()
                .ReverseMap();



        }

    }
}
