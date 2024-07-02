using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.Common.Interfaces;
using System.Text.Json.Serialization;

namespace RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Concrete
{
    public abstract class EFBaseEntity : IEFBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
