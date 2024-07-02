using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;
using RentACarNow.Persistence.Contexts.EfCoreContext;
using RentACarNow.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Persistence.Repositories.Write.EfCore
{
    public class EfCoreAdminWriteRepository : EfCoreBaseWriteRepository<Admin>, IEfCoreAdminWriteRepository
    {
        public EfCoreAdminWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
