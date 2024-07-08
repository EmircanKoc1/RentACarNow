using RentACarNow.APIs.ReadAPI.Application.DTOs;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetById
{
    public class GetByIdCustomerQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public ICollection<ClaimDTO> Claims { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

    }

}
