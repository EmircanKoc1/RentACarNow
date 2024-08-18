using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromCustomer
{
    public class DeleteClaimFromCustomerCommandRequestHandler : IRequestHandler<DeleteClaimFromCustomerCommandRequest, DeleteClaimFromCustomerCommandResponse>
    {

        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromCustomerCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        public DeleteClaimFromCustomerCommandRequestHandler(IEfCoreClaimWriteRepository writeRepository, ILogger<DeleteClaimFromCustomerCommandRequestHandler> logger, IRabbitMQMessageService messageService)
        {
            _writeRepository = writeRepository;
            _logger = logger;
            _messageService = messageService;
        }

        public async Task<DeleteClaimFromCustomerCommandResponse> Handle(DeleteClaimFromCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            //await _writeRepository.DeleteClaimFromCustomerAsync(request.ClaimId, request.CustomerId);


            //_messageService.SendEventQueue<ClaimDeletedFromCustomerEvent>(
            //  exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
            //  routingKey: RabbitMQRoutingKeys.CLAIM_DELETED_FROM_ADMIN_ROUTING_KEY,
            //  @event: new ClaimDeletedFromCustomerEvent
            //  {
            //      CustomerId = request.CustomerId,
            //      ClaimId = request.ClaimId
            //  });

            return new DeleteClaimFromCustomerCommandResponse();
        }
    }
}
