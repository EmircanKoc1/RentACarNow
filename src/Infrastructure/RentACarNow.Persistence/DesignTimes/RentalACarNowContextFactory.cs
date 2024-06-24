using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACarNow.Persistence.Contexts;

namespace RentACarNow.Persistence.DesignTimes
{
    public class RentalACarNowContextFactory : IDesignTimeDbContextFactory<RentalACarNowDbContext>
    {
        public RentalACarNowDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RentalACarNowDbContext>();
            optionsBuilder.UseSqlServer("");
            return new RentalACarNowDbContext(optionsBuilder.Options);

        }
    }
}
