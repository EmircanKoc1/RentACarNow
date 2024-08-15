using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimDeletedUser
{

    public class ClaimDeleteUserCommandHandler : IRequestHandler<ClaimDeleteUserCommandRequest, ClaimDeleteUserCommandResponse>
    {
        public Task<ClaimDeleteUserCommandResponse> Handle(ClaimDeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
