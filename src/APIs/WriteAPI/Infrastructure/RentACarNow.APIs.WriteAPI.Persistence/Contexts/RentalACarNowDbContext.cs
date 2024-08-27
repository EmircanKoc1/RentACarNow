using Microsoft.EntityFrameworkCore;
using RentACarNow.APIs.WriteAPI.Application.Contexts;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Contexts
{
    public class RentalACarNowDbContext : DbContext, IEfDBContext
    {
        public RentalACarNowDbContext(DbContextOptions options) : base(options)
        {

        }

        public RentalACarNowDbContext()
        {

        }

        //public DbSet<Admin> Admins { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Customer> Customers { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-V0R454\\MSSQL2022DEV;Database=RentACarNowDb;User ID=sa;Password=mssql2022dev;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .Property(x => x.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<User>()
              .Property(x => x.Id)
              .ValueGeneratedNever();

            modelBuilder.Entity<Car>()
              .Property(x => x.Id)
              .ValueGeneratedNever();

            modelBuilder.Entity<Claim>()
              .Property(x => x.Id)
              .ValueGeneratedNever();
           
            modelBuilder.Entity<Feature>()
              .Property(x => x.Id)
              .ValueGeneratedNever();


            modelBuilder.Entity<Rental>()
              .Property(x => x.Id)
              .ValueGeneratedNever();





            base.OnModelCreating(modelBuilder);
        }
    }
}
