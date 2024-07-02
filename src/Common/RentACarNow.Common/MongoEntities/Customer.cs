using RentACarNow.Common.MongoEntities.Common.Concrete;
using RentACarNow.Common.MongoEntities.Common.Interfaces;

namespace RentACarNow.Domain.Entities.MongoEntities
{
    public class Customer : BaseEntity, IMongoEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }

        ICollection<Claim> Claims { get; set; }
    }
}
