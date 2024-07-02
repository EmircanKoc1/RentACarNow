using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.MongoEntities
{
    public class Feature : MongoBaseEntity, IMongoEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Car Car { get; set; }
    }
}
