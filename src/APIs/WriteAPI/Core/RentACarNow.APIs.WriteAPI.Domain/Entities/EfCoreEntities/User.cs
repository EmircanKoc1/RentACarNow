using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class User : EFBaseEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public decimal WalletBalance { get; set; }

        public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>();
    }
}
