using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.CreateEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.DeleteEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.UpdateEmployee;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Employee;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class EmployeeMapProfile : Profile
    {
        public EmployeeMapProfile()
        {

            CreateMap<CreateEmployeeCommandRequest, Employee>();
            CreateMap<DeleteEmployeeCommandRequest, Employee>();
            CreateMap<UpdateEmployeeCommandRequest, Employee>();

            CreateMap<Employee, EmployeeDeletedEvent>();
            CreateMap<Employee, EmployeeAddedEvent>();
            CreateMap<Employee, EmployeeUpdatedEvent>();

        }
    }
}
