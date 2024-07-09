using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromAdmin
{
    public class DeleteClaimFromAdminCommandRequest
    {
        public Guid AdminId { get; set; }
        public Guid ClaimId { get; set; }

    }

    public class DeleteClaimFromEmployeeCommandResponse
    {

    }




}
