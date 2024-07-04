using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.UpdateRental
{
    public class UpdateRentalCommandRequestHandler : IRequestHandler<UpdateRentalCommandRequest, UpdateRentalCommandResponse>
    {
        private readonly IEfCoreRentalWriteRepository _writeRepository;
        private readonly IEfCoreRentalReadRepository _readRepository;
        private readonly IValidator<UpdateRentalCommandRequest> _validator;
        private readonly ILogger<UpdateRentalCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public UpdateRentalCommandRequestHandler(
            IEfCoreRentalWriteRepository writeRepository,
            IEfCoreRentalReadRepository readRepository,
            IValidator<UpdateRentalCommandRequest> validator,
            ILogger<UpdateRentalCommandRequestHandler> logger,
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

        public async Task<UpdateRentalCommandResponse> Handle(UpdateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateRentalCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateRentalCommandResponse { };


            var rentalEntity = _mapper.Map<EfEntity.Rental>(request);


            await _writeRepository.UpdateAsync(rentalEntity);
            await _writeRepository.SaveChangesAsync();

            var rentalUpdatedEvent = _mapper.Map<RentalUpdatedEvent>(rentalEntity);

            _messageService.SendEventQueue<RentalUpdatedEvent>(
                exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.RENTAL_UPDATED_ROUTING_KEY,
                @event: rentalUpdatedEvent);


            return new UpdateRentalCommandResponse { };
        }
    }

}
