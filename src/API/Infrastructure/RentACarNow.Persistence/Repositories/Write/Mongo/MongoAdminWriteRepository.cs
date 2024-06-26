using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;
using RentACarNow.Persistence.Contexts.MongoContexts;
using RentACarNow.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Persistence.Repositories.Write.Mongo
{
    public class MongoAdminWriteRepository : MongoBaseWriteRepository<Admin>, IMongoAdminWriteRepository
    {
        public MongoAdminWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
