using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.MongoEntities
{
    public class Feature : BaseEntity, IMongoEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Car Car { get; set; }
    }
}
