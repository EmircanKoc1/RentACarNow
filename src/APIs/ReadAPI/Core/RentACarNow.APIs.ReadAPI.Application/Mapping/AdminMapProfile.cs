using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll;
using RentACarNow.Common.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class AdminMapProfile : Profile
    {

        public AdminMapProfile()
        {

            CreateMap<Admin, GetAllAdminQueryResponse>();


        }

    }
}
