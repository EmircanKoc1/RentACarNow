using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.CreateFeature;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.DeleteFeature;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Feature;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.UpdateFeature
{
    public class UpdateFeatureCommandRequestHandler : IRequestHandler<UpdateFeatureCommandRequest, UpdateFeatureCommandResponse>
    {
        private readonly IEfCoreFeatureWriteRepository _writeRepository;
        private readonly IEfCoreFeatureReadRepository _readRepository;
        private readonly IValidator<UpdateFeatureCommandRequest> _validator;
        private readonly ILogger<UpdateFeatureCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public UpdateFeatureCommandRequestHandler(
            IEfCoreFeatureWriteRepository writeRepository,
            IEfCoreFeatureReadRepository readRepository,
            IValidator<UpdateFeatureCommandRequest> validator,
            ILogger<UpdateFeatureCommandRequestHandler> logger,
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

        public async Task<UpdateFeatureCommandResponse> Handle(UpdateFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateFeatureCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateFeatureCommandResponse { };


            var featureEntity = _mapper.Map<EfEntity.Feature>(request);


            await _writeRepository.UpdateAsync(featureEntity);
            await _writeRepository.SaveChangesAsync();

            var featureUpdatedEvent = _mapper.Map<FeatureUpdatedEvent>(featureEntity);

            _messageService.SendEventQueue<FeatureUpdatedEvent>(
                exchangeName: RabbitMQExchanges.FEATURE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.FEATURE_UPDATED_ROUTING_KEY,
                @event: featureUpdatedEvent);


            return new UpdateFeatureCommandResponse { };
        }
    }

}
