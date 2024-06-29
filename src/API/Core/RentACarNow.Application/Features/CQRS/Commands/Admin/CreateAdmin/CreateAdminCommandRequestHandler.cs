using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Application.Constants.Exchanges;
using RentACarNow.Application.Constants.RoutingKeys;
using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Application.Interfaces.Repositories.Write.EfCore;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Domain.Events.Admin;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequestHandler : IRequestHandler<CreateAdminCommandRequest, CreateAdminCommandResponse>
    {
        private readonly IEfCoreAdminWriteRepository _writeRepository;
        private readonly IEfCoreAdminReadRepository _readRepository;
        private readonly IValidator<CreateAdminCommandRequest> _validator;
        private readonly ILogger<CreateAdminCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public CreateAdminCommandRequestHandler(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<CreateAdminCommandResponse> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("naolndoas");

            _messageService.SendEventQueue(
                 exchangeName: RabbitMQExchanges.ADMIN_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.ADMIN_ADDED_ROUTING_KEY,
                @event: new AdminAddedEvent
                {
                    Email = "admin12l3n12kl@gmail.com",
                    Password="aknwkdnaksjnd",
                    Username="jkansjkdnsakdnjkadnjsakdnasjkdn"
                });

            return new CreateAdminCommandResponse();
        }
    }

}
