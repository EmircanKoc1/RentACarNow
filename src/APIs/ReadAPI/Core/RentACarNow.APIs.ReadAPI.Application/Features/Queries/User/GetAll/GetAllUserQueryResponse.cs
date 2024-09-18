using RentACarNow.APIs.ReadAPI.Application.DTOs;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetAll
{

    public class GetAllUserQueryResponse
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public ClaimDTO? Claims { get; set; }

    }

}
