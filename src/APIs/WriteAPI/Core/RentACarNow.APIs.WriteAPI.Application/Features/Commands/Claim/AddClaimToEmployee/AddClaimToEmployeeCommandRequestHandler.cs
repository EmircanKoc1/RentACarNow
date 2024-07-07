using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToEmployee
{
    public class AddClaimToEmployeeCommandRequestHandler : IRequestHandler<AddClaimToEmployeeCommandRequest, AddClaimToEmployeeCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _repository;
        private readonly IRabbitMQMessageService _messageService;
        private readonly ILogger<AddClaimToEmployeeCommandRequestHandler> _logger;

        public AddClaimToEmployeeCommandRequestHandler(IEfCoreClaimWriteRepository repository, IRabbitMQMessageService messageService, ILogger<AddClaimToEmployeeCommandRequestHandler> logger)
        {
            _repository = repository;
            _messageService = messageService;
            _logger = logger;
        }

        public async Task<AddClaimToEmployeeCommandResponse> Handle(AddClaimToEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var claim = await _repository.AddClaimToEmployeeAsync(request.ClaimId, request.EmployeeId);

            _messageService.SendEventQueue<ClaimAddedToEmployeeEvent>(
                exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CLAIM_ADDED_TO_EMPLOYEE_ROUTING_KEY,
                @event: new ClaimAddedToEmployeeEvent
                {
                    EmployeeId = request.EmployeeId,
                    ClaimId = claim.Id,
                    CreatedDate = claim.CreatedDate,
                    DeletedDate = claim.DeletedDate,
                    UpdatedDate = claim.UpdatedDate,
                    Key = claim.Key,
                    Value = claim.Value
                });

            return new AddClaimToEmployeeCommandResponse();
        }
    }
}
