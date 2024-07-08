using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetById;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class AdminMapProfile : Profile
    {

        public AdminMapProfile()
        {

            CreateMap<Admin, GetAllAdminQueryResponse>();
            CreateMap<Admin, GetByIdAdminQueryResponse>();


        }

    }
}
