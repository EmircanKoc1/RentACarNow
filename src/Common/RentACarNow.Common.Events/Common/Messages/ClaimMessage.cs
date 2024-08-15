namespace RentACarNow.Common.Events.Common.Messages
{
    public class ClaimMessage
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
