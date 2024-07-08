using RentACarNow.APIs.ReadAPI.Application.DTOs;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll
{
    public class GetAllAdminQueryResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<ClaimDTO> Claims { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

    }

}
