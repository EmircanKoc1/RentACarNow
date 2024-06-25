using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Interfaces.Repositories.Write.EfCore
{
    public interface IEfCoreAdminWriteRepository : IEfWriteRepository<Admin>
    {
    }
}
