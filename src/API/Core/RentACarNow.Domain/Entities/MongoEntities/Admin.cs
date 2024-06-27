using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.Domain.Entities.MongoEntities
{
    public class Admin : BaseEntity, IMongoEntity
    {
        
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
