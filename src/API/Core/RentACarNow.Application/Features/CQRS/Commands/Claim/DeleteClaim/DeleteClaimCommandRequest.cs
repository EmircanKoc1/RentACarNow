using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandRequest : IRequest<DeleteClaimCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
