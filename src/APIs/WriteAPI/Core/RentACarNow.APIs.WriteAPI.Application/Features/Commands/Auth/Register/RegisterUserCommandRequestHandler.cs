using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Auth.Register
{
    public class RegisterUserCommandRequestHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        public Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
