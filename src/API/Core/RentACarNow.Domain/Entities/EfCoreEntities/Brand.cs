using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;

namespace RentACarNow.Domain.Entities.EfCoreEntities
{
    public class Brand : BaseEntity,IEFEntity
    {
        public string Name{ get; set; }
        public string Description{ get; set; }

        ICollection<Car> Cars { get; set; } 

    }
}
