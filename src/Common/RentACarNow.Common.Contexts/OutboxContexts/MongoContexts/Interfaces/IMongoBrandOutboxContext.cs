using RentACarNow.Common.Entities.OutboxEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Contexts.OutboxContexts.MongoContexts.Interfaces
{
    public interface IMongoBrandOutboxContext : IBaseMongoOutboxContext<BrandOutboxMessage>
    {
    }
}
