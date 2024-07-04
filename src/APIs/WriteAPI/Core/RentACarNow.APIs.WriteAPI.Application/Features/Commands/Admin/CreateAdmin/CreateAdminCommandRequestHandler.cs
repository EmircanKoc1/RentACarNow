using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Admin;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequestHandler : IRequestHandler<CreateAdminCommandRequest, CreateAdminCommandResponse>
    {
        private readonly IEfCoreAdminWriteRepository _writeRepository;
        private readonly IEfCoreAdminReadRepository _readRepository;
        private readonly IValidator<CreateAdminCommandRequest> _validator;
        private readonly ILogger<CreateAdminCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public CreateAdminCommandRequestHandler(
            IEfCoreAdminWriteRepository writeRepository,
            IEfCoreAdminReadRepository readRepository,
            IValidator<CreateAdminCommandRequest> validator,
            ILogger<CreateAdminCommandRequestHandler> logger,
            IRabbitMQMessageService messageService,
            IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _validator = validator;
            _logger = logger;
            _messageService = messageService;
            _mapper = mapper;
        }

        public async Task<CreateAdminCommandResponse> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateAdminCommandResponse { };
            }


            var adminEntity = _mapper.Map<EfEntity.Admin>(request);


            await _writeRepository.AddAsync(adminEntity);
            await _writeRepository.SaveChangesAsync();

            var adminAddedEvent = _mapper.Map<AdminAddedEvent>(adminEntity);

            _messageService.SendEventQueue<AdminAddedEvent>(
                exchangeName: RabbitMQExchanges.ADMIN_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.ADMIN_ADDED_ROUTING_KEY,
                @event: adminAddedEvent);


            return new CreateAdminCommandResponse { };
        }
    }

}
