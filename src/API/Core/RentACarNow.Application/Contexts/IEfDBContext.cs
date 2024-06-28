﻿using Microsoft.EntityFrameworkCore;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.Application.Contexts
{
    public interface IEfDBContext
    {
        DbSet<Admin> Admins { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Car> Cars { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Feature> Features { get; set; }
        DbSet<Rental> Rentals { get; set; }

    }
}
