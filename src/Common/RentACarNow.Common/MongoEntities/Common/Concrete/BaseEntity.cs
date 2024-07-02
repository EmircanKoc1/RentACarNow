using RentACarNow.Common.MongoEntities.Common.Interfaces;
using System.Text.Json.Serialization;

namespace RentACarNow.Common.MongoEntities.Common.Concrete
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
