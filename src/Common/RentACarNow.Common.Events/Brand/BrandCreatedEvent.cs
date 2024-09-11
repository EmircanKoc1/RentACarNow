using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Events.Common.Messages;

namespace RentACarNow.Common.Events.Brand
{
    public sealed class BrandCreatedEvent : BaseEvent
    {

        public BrandCreatedEvent(
            Guid brandId, 
            string name, 
            string description, 
            DateTime createdDate)
        {
            BrandId = brandId;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
        }

        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

    }

}
