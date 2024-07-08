using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Employee.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Employee.GetById;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class EmployeeMapProfile : Profile
    {
        public EmployeeMapProfile()
        {
            CreateMap<Employee, GetByIdEmployeeQueryResponse>();
            CreateMap<Employee, GetAllEmployeeQueryResponse>();


        }


    }
}
