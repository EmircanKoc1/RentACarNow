namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById
{
    public class GetByIdClaimQueryResponse
    {
        public Guid ClaimId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }

}
