using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Persistence.Repositories.Read.EfCore
{
    public class EfCoreAdminReadRepository : EfCoreBaseReadRepository<Admin>,IEfCoreAdminReadRepository
    {
    }
    public class EfCoreBrandReadRepository : EfCoreBaseReadRepository<Brand>, IEfCoreBrandReadRepository
    {
    }
    public class EfCoreCarReadRepository : EfCoreBaseReadRepository<Car>, IEfCoreCarReadRepository
    {
    }
    public class EfCoreCustomerReadRepository : EfCoreBaseReadRepository<Customer>, IEfCoreCustomerReadRepository
    {
    }
    public class EfCoreEmployeeReadRepository : EfCoreBaseReadRepository<Employee>, IEfCoreEmployeeReadRepository
    {
    }
    public class EfCoreFeatureReadRepository : EfCoreBaseReadRepository<Feature>, IEfCoreFeatureReadRepository
    {
    }
    public class EfCoreRentalReadRepository : EfCoreBaseReadRepository<Rental>, IEfCoreRentalReadRepository
    {
    }
}
