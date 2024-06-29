﻿using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.Domain.Entities.EfCoreEntities
{
    public class Customer : BaseEntity, IEfEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public decimal WalletBalance { get; set; }

        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Claim> Claims { get; set; }

    }
}