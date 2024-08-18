namespace RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces
{
    public interface IEFBaseEntity
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
