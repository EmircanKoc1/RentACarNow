using MongoDB.Bson.Serialization.Attributes;

namespace RentACarNow.Common.Entities.InboxEntities
{
    public abstract class BaseInboxMessage : IInboxMessage
    {
        [BsonId]
        public Guid MessageId { get; set; }
        public string Payload { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? AddedDate { get; set; }
    }
}
