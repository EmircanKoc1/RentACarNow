using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromEmployee
{
    public class DeleteClaimFromEmployeeCommandRequestHandler : IRequestHandler<DeleteClaimFromEmployeeCommandRequest, DeleteClaimFromEmployeeCommandResponse>
    {

        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromEmployeeCommandRequestHandler> _logger;

        public DeleteClaimFromEmployeeCommandRequestHandler(IEfCoreClaimWriteRepository writeRepository, ILogger<DeleteClaimFromEmployeeCommandRequestHandler> logger)
        {
            _writeRepository = writeRepository;
            _logger = logger;
        }

        public async Task<DeleteClaimFromEmployeeCommandResponse> Handle(DeleteClaimFromEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            await _writeRepository.DeleteClaimFromEmployeeAsync(request.ClaimId, request.EmployeeId);

            return new DeleteClaimFromEmployeeCommandResponse();
        }
    }
}
