using Microsoft.EntityFrameworkCore;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.Application.Contexts
{
    public interface IEfDBContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Rental> Rentals { get; set; }

    }
}
