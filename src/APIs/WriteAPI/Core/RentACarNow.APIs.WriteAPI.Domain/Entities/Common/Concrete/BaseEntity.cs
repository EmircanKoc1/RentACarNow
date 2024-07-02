using RentACarNow.Domain.Entities.Common.Interfaces;
using System.Text.Json.Serialization;

namespace RentACarNow.Domain.Entities.Common.Concrete
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
