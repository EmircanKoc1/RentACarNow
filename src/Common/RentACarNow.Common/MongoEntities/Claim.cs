using RentACarNow.Common.MongoEntities.Common.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Domain.Entities.MongoEntities
{
    public class Claim : BaseEntity
    {

        public string Key { get; set; }
        public string Value { get; set; }

    }
}
