using RentACarNow.APIs.ReadAPI.Application.DTOs;
using RentACarNow.Common.Enums.EntityEnums;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Employee.GetById
{
    public class GetByIdEmployeeQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EmployeePosition Position { get; set; }
        public WorkEnvironment WorkEnvironment { get; set; }
        public ICollection<ClaimDTO> Claims { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; } 
        public DateTime? DeletedDate { get; set; }

    }

}
