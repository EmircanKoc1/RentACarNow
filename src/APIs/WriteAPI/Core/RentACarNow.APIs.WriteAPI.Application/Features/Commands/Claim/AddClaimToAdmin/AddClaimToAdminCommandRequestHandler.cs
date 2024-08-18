using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToAdmin
{
    public class AddClaimToAdminCommandRequestHandler : IRequestHandler<AddClaimToAdminCommandRequest, AddClaimToAdminCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _repository;
        private readonly IRabbitMQMessageService _messageService;
        private readonly ILogger<AddClaimToAdminCommandRequestHandler> _logger;

        public AddClaimToAdminCommandRequestHandler(IEfCoreClaimWriteRepository repository, IRabbitMQMessageService messageService, ILogger<AddClaimToAdminCommandRequestHandler> logger)
        {
            _repository = repository;
            _messageService = messageService;
            _logger = logger;
        }

        public async Task<AddClaimToAdminCommandResponse> Handle(AddClaimToAdminCommandRequest request, CancellationToken cancellationToken)
        {

            //var claim = await _repository.AddClaimToAdminAsync(request.ClaimId, request.AdminId);

            //_messageService.SendEventQueue<ClaimAddedToAdminEvent>(
            //    exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
            //    routingKey: RabbitMQRoutingKeys.CLAIM_ADDED_TO_ADMIN_ROUTING_KEY,
            //    @event: new ClaimAddedToAdminEvent
            //    {
            //        AdminId = request.AdminId,
            //        ClaimId = claim.Id,
            //        CreatedDate = claim.CreatedDate,
            //        DeletedDate = claim.DeletedDate,
            //        UpdatedDate = claim.UpdatedDate,
            //        Key = claim.Key,
            //        Value = claim.Value
            //    });


            return new AddClaimToAdminCommandResponse();
        }
    }
}
