using RentACarNow.Common.MongoEntities;
using RentACarNow.Common.MongoEntities.Common.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Entities.MongoEntities
{
    public class User : MongoBaseEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public decimal WalletBalance { get; set; }

        public ICollection<Claim> Claims { get; set; }

    }
}
