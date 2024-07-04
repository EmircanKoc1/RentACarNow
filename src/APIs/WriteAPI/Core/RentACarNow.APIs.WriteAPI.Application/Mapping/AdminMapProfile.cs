using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.DeleteAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.UpdateAdmin;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Admin;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class AdminMapProfile : Profile
    {
        public AdminMapProfile()
        {

            CreateMap<CreateAdminCommandRequest, Admin>();
            CreateMap<DeleteAdminCommandRequest, Admin>();
            CreateMap<UpdateAdminCommandRequest, Admin>();

            CreateMap<Admin, AdminAddedEvent>();
            CreateMap<Admin, AdminDeletedEvent>();
            CreateMap<Admin, AdminUpdatedEvent>();

        }

    }
}
