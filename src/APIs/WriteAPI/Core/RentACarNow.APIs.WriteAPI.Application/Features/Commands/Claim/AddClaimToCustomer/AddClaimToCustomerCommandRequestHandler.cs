using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToCustomer
{
    public class AddClaimToCustomerCommandRequestHandler : IRequestHandler<AddClaimToCustomerCommandRequest, AddClaimToCustomerCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _repository;
        private readonly IRabbitMQMessageService _messageService;
        private readonly ILogger<AddClaimToCustomerCommandRequestHandler> _logger;

        public AddClaimToCustomerCommandRequestHandler(IEfCoreClaimWriteRepository repository, IRabbitMQMessageService messageService, ILogger<AddClaimToCustomerCommandRequestHandler> logger)
        {
            _repository = repository;
            _messageService = messageService;
            _logger = logger;
        }

        public async Task<AddClaimToCustomerCommandResponse> Handle(AddClaimToCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var claim = await _repository.AddClaimToCustomerAsync(request.ClaimId, request.CustomerId);

            _messageService.SendEventQueue<ClaimAddedToCustomerEvent>(
                exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CLAIM_ADDED_TO_CUSTOMER_ROUTING_KEY,
                @event: new ClaimAddedToCustomerEvent
                {
                    CustomerId = request.CustomerId,
                    ClaimId = claim.Id,
                    CreatedDate = claim.CreatedDate,
                    DeletedDate = claim.DeletedDate,
                    UpdatedDate = claim.UpdatedDate,
                    Key = claim.Key,
                    Value = claim.Value
                });

            return new AddClaimToCustomerCommandResponse();
        }
    }
}
