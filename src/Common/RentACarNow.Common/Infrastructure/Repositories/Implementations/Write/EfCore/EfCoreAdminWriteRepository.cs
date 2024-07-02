using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.EfCore
{
    public class EfCoreAdminWriteRepository : EfCoreBaseWriteRepository<Admin>, IEfCoreAdminWriteRepository
    {
        public EfCoreAdminWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
