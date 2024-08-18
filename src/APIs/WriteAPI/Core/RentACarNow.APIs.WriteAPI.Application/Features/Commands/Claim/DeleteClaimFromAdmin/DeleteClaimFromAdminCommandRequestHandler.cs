using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromAdmin
{
    public class DeleteClaimFromAdminCommandRequestHandler : IRequestHandler<DeleteClaimFromAdminCommandRequest, DeleteClaimFromAdminCommandResponse>
    {

        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromAdminCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public DeleteClaimFromAdminCommandRequestHandler(IEfCoreClaimWriteRepository writeRepository, ILogger<DeleteClaimFromAdminCommandRequestHandler> logger, IRabbitMQMessageService messageService)
        {
            _writeRepository = writeRepository;
            _logger = logger;
            _messageService = messageService;
        }

        public async Task<DeleteClaimFromAdminCommandResponse> Handle(DeleteClaimFromAdminCommandRequest request, CancellationToken cancellationToken)
        {
            //await _writeRepository.DeleteClaimFromAdminAsync(request.ClaimId, request.AdminId);


            //_messageService.SendEventQueue<ClaimDeletedFromAdminEvent>(
            //    exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
            //    routingKey: RabbitMQRoutingKeys.CLAIM_DELETED_FROM_ADMIN_ROUTING_KEY,
            //    @event: new ClaimDeletedFromAdminEvent
            //    {
            //        AdminId = request.AdminId,
            //        ClaimId = request.ClaimId
            //    });


            return new DeleteClaimFromAdminCommandResponse();

        }
    }
}
