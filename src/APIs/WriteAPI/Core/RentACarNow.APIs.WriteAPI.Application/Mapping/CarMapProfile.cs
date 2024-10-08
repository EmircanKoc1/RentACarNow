﻿using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class CarMapProfile : Profile
    {
        public CarMapProfile()
        {

            CreateMap<Car, CarMessage>()
                .ReverseMap();

            CreateMap<Feature, FeatureMessage>()
                .ReverseMap();

            CreateMap<Brand, BrandMessage>()
                .ReverseMap();

            CreateMap<CreateCarCommandRequest, Car>();
            CreateMap<DeleteCarCommandRequest, Car>();
            CreateMap<UpdateCarCommandRequest, Car>();

            CreateMap<Car, CarCreatedEvent>();
            CreateMap<Car, CarDeletedEvent>();
            CreateMap<Car, CarUpdatedEvent>();

        }

    }
}
