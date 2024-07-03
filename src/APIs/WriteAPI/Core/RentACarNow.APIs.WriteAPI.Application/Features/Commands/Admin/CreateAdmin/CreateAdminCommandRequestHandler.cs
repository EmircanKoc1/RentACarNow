using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.Exchanges;
using RentACarNow.Common.Constants.RoutingKeys;
using RentACarNow.Common.Events.Admin;
using RentACarNow.Common.Infrastructure.Services;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin
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
                    Password = "aknwkdnaksjnd",
                    Username = "jkansjkdnsakdnjkadnjsakdnasjkdn"
                });

            return new CreateAdminCommandResponse();
        }
    }

}
