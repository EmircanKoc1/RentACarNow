using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoAdminWriteRepository : MongoBaseWriteRepository<Admin>, IMongoAdminWriteRepository
    {
        public MongoAdminWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
