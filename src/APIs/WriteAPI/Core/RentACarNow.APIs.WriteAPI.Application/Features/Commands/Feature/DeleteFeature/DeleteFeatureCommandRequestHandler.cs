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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.DeleteFeature
{
    public class DeleteFeatureCommandRequestHandler : IRequestHandler<DeleteFeatureCommandRequest, DeleteFeatureCommandResponse>
    {
        private readonly IEfCoreFeatureWriteRepository _writeRepository;
        private readonly IEfCoreFeatureReadRepository _readRepository;
        private readonly IValidator<DeleteFeatureCommandRequest> _validator;
        private readonly ILogger<DeleteFeatureCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public DeleteFeatureCommandRequestHandler(
            IEfCoreFeatureWriteRepository writeRepository,
            IEfCoreFeatureReadRepository readRepository,
            IValidator<DeleteFeatureCommandRequest> validator,
            ILogger<DeleteFeatureCommandRequestHandler> logger,
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

        public async Task<DeleteFeatureCommandResponse> Handle(DeleteFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteFeatureCommandResponse();
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                return new DeleteFeatureCommandResponse();
            }

            var featureEntity = _mapper.Map<EfEntity.Feature>(request);

            _writeRepository.Delete(featureEntity);
            _writeRepository.SaveChanges();

            var featureDeletedEvent = _mapper.Map<FeatureDeletedEvent>(featureEntity);

            _messageService.SendEventQueue<FeatureDeletedEvent>(
                exchangeName: RabbitMQExchanges.FEATURE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.FEATURE_DELETED_ROUTING_KEY,
                @event: featureDeletedEvent);

            return new DeleteFeatureCommandResponse();
        }
    }
}
