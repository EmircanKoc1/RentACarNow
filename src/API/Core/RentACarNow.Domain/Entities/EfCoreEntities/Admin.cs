﻿using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.Domain.Entities.EfCoreEntities
{
    public class Admin : BaseEntity, IEFEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        ICollection<Claim> Claims { get; set; }


    }
}
