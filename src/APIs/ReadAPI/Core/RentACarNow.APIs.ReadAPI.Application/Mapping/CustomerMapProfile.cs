using Amazon.Runtime.Internal;
using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetById;
using RentACarNow.Common.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class CustomerMapProfile : Profile 
    {
        public CustomerMapProfile()
        {
            CreateMap<CustomerDTO, Customer>()
                .ReverseMap();

            CreateMap<Customer, GetAllCustomerQueryResponse>();
            CreateMap<Customer, GetByIdCustomerQueryResponse>();

        }

    }
}
