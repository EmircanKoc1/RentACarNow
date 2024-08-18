using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaimFromEmployee
{
    public class DeleteClaimFromEmployeeCommandRequestHandler : IRequestHandler<DeleteClaimFromEmployeeCommandRequest, DeleteClaimFromEmployeeCommandResponse>
    {

        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromEmployeeCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public DeleteClaimFromEmployeeCommandRequestHandler(IEfCoreClaimWriteRepository writeRepository, ILogger<DeleteClaimFromEmployeeCommandRequestHandler> logger, IRabbitMQMessageService messageService)
        {
            _writeRepository = writeRepository;
            _logger = logger;
            _messageService = messageService;
        }

        public async Task<DeleteClaimFromEmployeeCommandResponse> Handle(DeleteClaimFromEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            //await _writeRepository.DeleteClaimFromEmployeeAsync(request.ClaimId, request.EmployeeId);

            //_messageService.SendEventQueue<ClaimDeletedFromEmployeeEvent>(
            //  exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
            //  routingKey: RabbitMQRoutingKeys.CLAIM_DELETED_FROM_EMPLOYEE_ROUTING_KEY,
            //  @event: new ClaimDeletedFromEmployeeEvent
            //  {
            //      EmployeeId = request.EmployeeId,
            //      ClaimId = request.ClaimId
            //  });


            return new DeleteClaimFromEmployeeCommandResponse();
        }
    }
}
