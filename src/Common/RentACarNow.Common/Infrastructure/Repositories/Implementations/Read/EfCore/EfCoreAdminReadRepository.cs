using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using RentACarNow.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Persistence.Repositories.Read.EfCore
{
    public class EfCoreAdminReadRepository : EfCoreBaseReadRepository<Admin>, IEfCoreAdminReadRepository
    {
        public EfCoreAdminReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
