﻿using MediatR;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim;
using RentACarNow.Common.Enums.EntityEnums;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.CreateEmployee
{
    public class CreateEmployeeCommandRequest : IRequest<CreateEmployeeCommandResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public EmployeePosition Position { get; set; }
        public WorkEnvironment WorkEnvironment { get; set; }
        public Guid ClaimId { get; set; }

        public ICollection<CreateClaimCommandRequest> Claims { get; set; }

    }

}
