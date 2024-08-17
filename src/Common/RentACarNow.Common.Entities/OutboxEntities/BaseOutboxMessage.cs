namespace RentACarNow.Common.Entities.OutboxEntities
{
    public abstract class BaseOutboxMessage : IOutboxMessage
    {
        public Guid Id { get; set; }
        public string Payload { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? AddedDate { get; set; }
    }
}
