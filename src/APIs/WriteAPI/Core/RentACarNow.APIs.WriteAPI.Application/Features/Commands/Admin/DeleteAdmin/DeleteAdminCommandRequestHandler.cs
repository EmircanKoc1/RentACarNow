using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.CreateAdmin;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Admin;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;



namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.DeleteAdmin
{
    public class DeleteAdminCommandRequestHandler : IRequestHandler<DeleteAdminCommandRequest, DeleteAdminCommandResponse>
    {
        private readonly IEfCoreAdminWriteRepository _writeRepository;
        private readonly IEfCoreAdminReadRepository _readRepository;
        private readonly IValidator<DeleteAdminCommandRequest> _validator;
        private readonly ILogger<DeleteAdminCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public DeleteAdminCommandRequestHandler(
            IEfCoreAdminWriteRepository writeRepository,
            IEfCoreAdminReadRepository readRepository,
            IValidator<DeleteAdminCommandRequest> validator,
            ILogger<DeleteAdminCommandRequestHandler> logger,
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

        public async Task<DeleteAdminCommandResponse> Handle(DeleteAdminCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteAdminCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new DeleteAdminCommandResponse { };


            var adminEntity = _mapper.Map<EfEntity.Admin>(request);


            _writeRepository.Delete(adminEntity);
            _writeRepository.SaveChanges();

            var adminDeletedEvent = _mapper.Map<AdminDeletedEvent>(adminEntity);

            _messageService.SendEventQueue<AdminDeletedEvent>(
                exchangeName: RabbitMQExchanges.ADMIN_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.ADMIN_DELETED_ROUTING_KEY,
                @event: adminDeletedEvent);


            return new DeleteAdminCommandResponse { };
        }
    }

}
