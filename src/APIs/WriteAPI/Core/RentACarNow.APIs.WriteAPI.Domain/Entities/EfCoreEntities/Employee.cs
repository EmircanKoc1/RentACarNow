using RentACarNow.Common.Enums.EntityEnums;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Employee : Common.Concrete.EFBaseEntity, Common.Interfaces.IEFEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public EmployeePosition Position { get; set; }
        public WorkEnvironment WorkEnvironment { get; set; }

        public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>();
    }
}
