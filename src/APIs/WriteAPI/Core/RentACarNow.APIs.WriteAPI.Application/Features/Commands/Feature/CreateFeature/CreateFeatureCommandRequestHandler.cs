using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Feature; 
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.CreateFeature
{
    public class CreateFeatureCommandRequestHandler : IRequestHandler<CreateFeatureCommandRequest, CreateFeatureCommandResponse>
    {
        private readonly IEfCoreFeatureWriteRepository _writeRepository;
        private readonly IEfCoreFeatureReadRepository _readRepository;
        private readonly IValidator<CreateFeatureCommandRequest> _validator;
        private readonly ILogger<CreateFeatureCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public CreateFeatureCommandRequestHandler(
            IEfCoreFeatureWriteRepository writeRepository,
            IEfCoreFeatureReadRepository readRepository,
            IValidator<CreateFeatureCommandRequest> validator,
            ILogger<CreateFeatureCommandRequestHandler> logger,
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

        public async Task<CreateFeatureCommandResponse> Handle(CreateFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateFeatureCommandResponse();
            }

            var featureEntity = _mapper.Map<EfEntity.Feature>(request);

            await _writeRepository.AddAsync(featureEntity);
            await _writeRepository.SaveChangesAsync();

            var featureAddedEvent = _mapper.Map<FeatureAddedEvent>(featureEntity);

            _messageService.SendEventQueue<FeatureAddedEvent>(
                exchangeName: RabbitMQExchanges.FEATURE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.FEATURE_ADDED_ROUTING_KEY,
                @event: featureAddedEvent);

            return new CreateFeatureCommandResponse();
        }
    }
}
