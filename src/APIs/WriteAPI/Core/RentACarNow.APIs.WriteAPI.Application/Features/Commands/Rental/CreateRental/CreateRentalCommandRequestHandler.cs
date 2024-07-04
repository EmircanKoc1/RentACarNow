using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Rental; // Assuming Rental events namespace
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequestHandler : IRequestHandler<CreateRentalCommandRequest, CreateRentalCommandResponse>
    {
        private readonly IEfCoreRentalWriteRepository _writeRepository;
        private readonly IEfCoreRentalReadRepository _readRepository;
        private readonly IValidator<CreateRentalCommandRequest> _validator;
        private readonly ILogger<CreateRentalCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public CreateRentalCommandRequestHandler(
            IEfCoreRentalWriteRepository writeRepository,
            IEfCoreRentalReadRepository readRepository,
            IValidator<CreateRentalCommandRequest> validator,
            ILogger<CreateRentalCommandRequestHandler> logger,
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

        public async Task<CreateRentalCommandResponse> Handle(CreateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateRentalCommandResponse();
            }

            var rentalEntity = _mapper.Map<EfEntity.Rental>(request);

            await _writeRepository.AddAsync(rentalEntity);
            await _writeRepository.SaveChangesAsync();

            var rentalAddedEvent = _mapper.Map<RentalAddedEvent>(rentalEntity);

            _messageService.SendEventQueue<RentalAddedEvent>(
                exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.RENTAL_ADDED_ROUTING_KEY,
                @event: rentalAddedEvent);

            return new CreateRentalCommandResponse();
        }
    }
}
