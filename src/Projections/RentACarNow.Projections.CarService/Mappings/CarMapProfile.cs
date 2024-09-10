using AutoMapper;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Projections.CarService.Mappings
{
    internal class CarMapProfile : Profile
    {
        public CarMapProfile()
        {
            CreateMap<CarCreatedEvent, Car>();
            CreateMap<CarDeletedEvent, Car>();
            CreateMap<CarUpdatedEvent, Car>();
            
        }
    }
}
