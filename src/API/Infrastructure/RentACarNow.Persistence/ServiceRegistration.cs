using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Persistence
{
    public static class ServiceRegistration
    {

        public static IServiceCollection ConfigPersistenceServices(this IServiceCollection services , IConfiguration configuration)
        {
            return services.AddDbContext<RentalACarNowDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

        }
    }
}
