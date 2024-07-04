using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar
{
    public class UpdateCarCommandRequestHandler : IRequestHandler<UpdateCarCommandRequest, UpdateCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _writeRepository;
        private readonly IEfCoreCarReadRepository _readRepository;
        private readonly IValidator<UpdateCarCommandRequest> _validator;
        private readonly ILogger<UpdateCarCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public UpdateCarCommandRequestHandler(
            IEfCoreCarWriteRepository writeRepository,
            IEfCoreCarReadRepository readRepository,
            IValidator<UpdateCarCommandRequest> validator,
            ILogger<UpdateCarCommandRequestHandler> logger,
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

        public async Task<UpdateCarCommandResponse> Handle(UpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateCarCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateCarCommandResponse { };


            var carEntity = _mapper.Map<EfEntity.Car>(request);


            await _writeRepository.UpdateAsync(carEntity);
            await _writeRepository.SaveChangesAsync();

            var carUpdatedEvent = _mapper.Map<CarUpdatedEvent>(carEntity);

            _messageService.SendEventQueue<CarUpdatedEvent>(
                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CAR_UPDATED_ROUTING_KEY,
                @event: carUpdatedEvent);


            return new UpdateCarCommandResponse { };
        }
    }

}
