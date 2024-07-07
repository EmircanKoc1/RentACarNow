using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Car; // Assuming Car events namespace
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar
{
    public class DeleteCarCommandRequestHandler : IRequestHandler<DeleteCarCommandRequest, DeleteCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _writeRepository;
        private readonly IEfCoreCarReadRepository _readRepository;
        private readonly IValidator<DeleteCarCommandRequest> _validator;
        private readonly ILogger<DeleteCarCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public DeleteCarCommandRequestHandler(
            IEfCoreCarWriteRepository writeRepository,
            IEfCoreCarReadRepository readRepository,
            IValidator<DeleteCarCommandRequest> validator,
            ILogger<DeleteCarCommandRequestHandler> logger,
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

        public async Task<DeleteCarCommandResponse> Handle(DeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteCarCommandResponse();
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                return new DeleteCarCommandResponse();
            }

            var carEntity = _mapper.Map<EfEntity.Car>(request);

            _writeRepository.Delete(carEntity);
            _writeRepository.SaveChanges();

            var carDeletedEvent = _mapper.Map<CarDeletedEvent>(carEntity);

            _messageService.SendEventQueue<CarDeletedEvent>(
                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CAR_DELETED_ROUTING_KEY,
                @event: carDeletedEvent);

            return new DeleteCarCommandResponse();
        }
    }
}
