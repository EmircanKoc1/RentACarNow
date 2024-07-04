using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.DeleteAdmin;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Admin;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;



namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.UpdateAdmin
{
    public class UpdateAdminCommandRequestHandler : IRequestHandler<UpdateAdminCommandRequest, UpdateAdminCommandResponse>
    {
        private readonly IEfCoreAdminWriteRepository _writeRepository;
        private readonly IEfCoreAdminReadRepository _readRepository;
        private readonly IValidator<UpdateAdminCommandRequest> _validator;
        private readonly ILogger<UpdateAdminCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public UpdateAdminCommandRequestHandler(
            IEfCoreAdminWriteRepository writeRepository,
            IEfCoreAdminReadRepository readRepository,
            IValidator<UpdateAdminCommandRequest> validator,
            ILogger<UpdateAdminCommandRequestHandler> logger,
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

        public async Task<UpdateAdminCommandResponse> Handle(UpdateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateAdminCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateAdminCommandResponse { };


            var adminEntity = _mapper.Map<EfEntity.Admin>(request);


            await _writeRepository.UpdateAsync(adminEntity);
            await _writeRepository.SaveChangesAsync();

            var adminUpdatedEvent = _mapper.Map<AdminUpdatedEvent>(adminEntity);

            _messageService.SendEventQueue<AdminUpdatedEvent>(
                exchangeName: RabbitMQExchanges.ADMIN_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.ADMIN_UPDATED_ROUTING_KEY,
                @event: adminUpdatedEvent);


            return new UpdateAdminCommandResponse { };
        }
    }

}
