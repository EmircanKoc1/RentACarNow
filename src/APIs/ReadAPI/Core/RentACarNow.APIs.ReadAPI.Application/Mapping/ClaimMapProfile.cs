using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;
using RentACarNow.Common.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Mapping
{
    public class ClaimMapProfile : Profile
    {

        public ClaimMapProfile()
        {
            CreateMap<ClaimDTO, Claim>()
                .ReverseMap();

            CreateMap<Claim, GetAllClaimQueryResponse>()
                .ForMember(dest => dest.ClaimId, src => src.MapFrom(c => c.Id));

            CreateMap<Claim, GetByIdClaimQueryResponse>()
               .ForMember(dest => dest.ClaimId, src => src.MapFrom(c => c.Id));



        }

    }
}
