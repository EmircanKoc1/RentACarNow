using RentACarNow.APIs.WriteAPI.Persistence.Contexts.MongoContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.Mongo
{
    public class MongoAdminWriteRepository : MongoBaseWriteRepository<Admin>, IMongoAdminWriteRepository
    {
        public MongoAdminWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
