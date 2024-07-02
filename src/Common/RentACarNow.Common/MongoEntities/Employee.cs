﻿using RentACarNow.Common.Enums;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.Common.MongoEntities
{
    public class Employee : BaseEntity, IEfEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public EmployeePosition Position { get; set; }
        public WorkEnvironment WorkEnvironment { get; set; }

        public ICollection<Claim> Claims { get; set; }
    }
}