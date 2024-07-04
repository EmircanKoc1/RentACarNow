using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.CreateCustomer;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.DeleteCustomer;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.UpdateCustomer;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Customer;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {

            CreateMap<CreateCustomerCommandRequest, Customer>();
            CreateMap<DeleteCustomerCommandRequest, Customer>();
            CreateMap<UpdateCustomerCommandRequest, Customer>();

            CreateMap<Customer, CustomerDeletedEvent>();
            CreateMap<Customer, CustomerUpdatedEvent>();
            CreateMap<Customer, CustomerAddedEvent>();

        }


    }
}
