using AutoMapper;
using RentACarNow.APIs.ReadAPI.Application.DTOs;
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


        }

    }
}
