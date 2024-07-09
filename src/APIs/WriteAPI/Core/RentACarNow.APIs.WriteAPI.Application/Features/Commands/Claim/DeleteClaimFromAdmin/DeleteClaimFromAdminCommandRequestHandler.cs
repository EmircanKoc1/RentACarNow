using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromAdmin
{
    public class DeleteClaimFromAdminCommandRequestHandler : IRequestHandler<DeleteClaimFromAdminCommandRequest, DeleteClaimFromAdminCommandResponse>
    {

        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromAdminCommandRequestHandler> _logger;

        public DeleteClaimFromAdminCommandRequestHandler(IEfCoreClaimWriteRepository writeRepository, ILogger<DeleteClaimFromAdminCommandRequestHandler> logger)
        {
            _writeRepository = writeRepository;
            _logger = logger;
        }

        public async Task<DeleteClaimFromAdminCommandResponse> Handle(DeleteClaimFromAdminCommandRequest request, CancellationToken cancellationToken)
        {
            await _writeRepository.DeleteClaimFromAdminAsync(request.ClaimId, request.AdminId);


            return new DeleteClaimFromAdminCommandResponse();

        }
    }
}
