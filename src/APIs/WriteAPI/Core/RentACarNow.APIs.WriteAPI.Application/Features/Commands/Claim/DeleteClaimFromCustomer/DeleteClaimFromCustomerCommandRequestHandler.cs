using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromCustomer
{
    public class DeleteClaimFromCustomerCommandRequestHandler : IRequestHandler<DeleteClaimFromCustomerCommandRequest, DeleteClaimFromCustomerCommandResponse>
    {

        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromCustomerCommandRequestHandler> _logger;
            
        public DeleteClaimFromCustomerCommandRequestHandler(IEfCoreClaimWriteRepository writeRepository, ILogger<DeleteClaimFromCustomerCommandRequestHandler> logger)
        {
            _writeRepository = writeRepository;
            _logger = logger;
        }

        public async Task<DeleteClaimFromCustomerCommandResponse> Handle(DeleteClaimFromCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            await _writeRepository.DeleteClaimFromCustomerAsync(request.ClaimId, request.CustomerId);

            return new DeleteClaimFromCustomerCommandResponse();
        }
    }
}
