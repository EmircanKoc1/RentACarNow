using AutoMapper;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.DeleteAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.UpdateAdmin;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.Common.Events.Admin;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.APIs.WriteAPI.Application.Mapping
{
    public class AdminMapProfile : Profile
    {
        public AdminMapProfile()
        {

            CreateMap<Claim, ClaimMessage>();
           


            CreateMap<CreateAdminCommandRequest, Admin>()
                .ForMember(dest => dest.Claims, source => source.MapFrom(s => s.Claims));


            CreateMap<DeleteAdminCommandRequest, Admin>();

            CreateMap<UpdateAdminCommandRequest, Admin>()
                 .ForMember(dest => dest.Claims, source => source.MapFrom(s => s.Claims));

            CreateMap<Admin, AdminAddedEvent>()
                 .ForMember(dest => dest.Claims, source => source.MapFrom(s => s.Claims));

            CreateMap<Admin, AdminDeletedEvent>();

            CreateMap<Admin, AdminUpdatedEvent>()
                 .ForMember(dest => dest.Claims, source => source.MapFrom(s => s.Claims));



        }

    }
}
