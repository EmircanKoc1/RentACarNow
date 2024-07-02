using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.MongoEntities
{
    public class Admin : BaseEntity, IMongoEntity
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Claim> Claims { get; set; }


    }
}
