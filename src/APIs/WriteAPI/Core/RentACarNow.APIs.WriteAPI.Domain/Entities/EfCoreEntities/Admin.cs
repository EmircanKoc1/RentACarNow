using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;


namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Admin : EFBaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>();


    }
}
