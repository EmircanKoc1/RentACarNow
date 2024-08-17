using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified
{
    public interface ICarOutboxRepository : IBaseOutboxRepository<CarOutboxMessage>
    {

    }

}
