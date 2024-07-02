using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandRequest : IRequest<CreateClaimCommandResponse>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

}
