using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Common.MongoEntities
{
    public class Admin : MongoBaseEntity, IMongoEntity
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Claim> Claims { get; set; }


    }
}
