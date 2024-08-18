using Microsoft.EntityFrameworkCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;


namespace RentACarNow.APIs.WriteAPI.Application.Contexts
{
    public interface IEfDBContext
    {
        //DbSet<Admin> Admins { get; set; }
        //DbSet<Customer> Customers { get; set; }
        //DbSet<Employee> Employees { get; set; }
        DbSet<User> Users { get; set; } 

        DbSet<Brand> Brands { get; set; }
        DbSet<Car> Cars { get; set; }
        DbSet<Feature> Features { get; set; }
        DbSet<Rental> Rentals { get; set; }
        DbSet<Claim> Claims { get; set; }


    }
}
