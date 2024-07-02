using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Customer : Common.Concrete.EFBaseEntity, Common.Interfaces.IEFEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public decimal WalletBalance { get; set; }

        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Claim> Claims { get; set; }

    }
}
