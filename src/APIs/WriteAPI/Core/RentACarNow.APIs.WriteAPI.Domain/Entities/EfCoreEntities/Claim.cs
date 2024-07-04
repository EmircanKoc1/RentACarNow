using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities
{
    public class Claim : EFBaseEntity ,IEFEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }

        ICollection<Admin> Admins { get; set; }
        ICollection<Customer> Customers { get; set; }
        ICollection<Employee> Employees { get; set; }
    }
}
