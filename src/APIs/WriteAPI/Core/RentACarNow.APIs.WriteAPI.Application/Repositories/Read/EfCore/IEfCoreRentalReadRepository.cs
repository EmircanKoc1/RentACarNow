using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore
{
    public interface IEfCoreRentalReadRepository : IEfReadRepository<Rental>
    {
    }
}
