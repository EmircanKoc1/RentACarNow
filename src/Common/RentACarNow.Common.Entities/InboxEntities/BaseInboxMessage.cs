﻿namespace RentACarNow.Common.Entities.InboxEntities
{
    public abstract class BaseInboxMessage : IInboxMessage
    {
        public Guid Id { get; set; }
        public string Payload { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? AddedDate { get; set; }
    }
}