﻿using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;


namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Admin : EFBaseEntity, IEFEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        ICollection<Claim> Claims { get; set; }


    }
}