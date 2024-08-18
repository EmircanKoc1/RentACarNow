using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Claim : EFBaseEntity, IEFEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }

        //public ICollection<Admin> Admins { get; set; } = new HashSet<Admin>();
        //public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
        //public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
