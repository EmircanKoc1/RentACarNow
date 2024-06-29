using Amazon.Runtime.Internal.Util;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequestHandler : IRequestHandler<CreateAdminCommandRequest, CreateAdminCommandResponse>
    {
        private readonly IEfCoreAdminWriteRepository _writeRepository;
        private readonly IEfCoreAdminReadRepository _readRepository;
        private readonly IValidator<CreateAdminCommandRequest> _validator;
        private readonly ILogger<CreateAdminCommandRequestHandler> _logger;



        public Task<CreateAdminCommandResponse> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("naolndoas");

            return null;
        }
    }

}
