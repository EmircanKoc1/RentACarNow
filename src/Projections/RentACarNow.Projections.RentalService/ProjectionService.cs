using AutoMapper;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoEntities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using RentACarNow.Common.Enums.RepositoryEnums;

namespace RentACarNow.Projections.RentalService
{
    public class ProjectionService : BackgroundService
    {
        private readonly IRentalInboxRepository _rentalInboxRepository;
        private readonly ILogger<ProjectionService> _logger;
        private readonly IMongoRentalWriteRepository _rentalWriteRepository;
        private readonly IMapper _mapper;

        public ProjectionService(
            IRentalInboxRepository rentalInboxRepository,
            ILogger<ProjectionService> logger,
            IMongoRentalWriteRepository rentalWriteRepository,
            IMapper mapper)
        {
            _rentalInboxRepository = rentalInboxRepository;
            _logger = logger;
            _rentalWriteRepository = rentalWriteRepository;
            _mapper = mapper;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ProjectionService)} execute method has been executed");

            while (!stoppingToken.IsCancellationRequested)
            {
                var inboxMessages = await _rentalInboxRepository
                   .GetMessagesAsync(5, OrderedDirection.None);

                foreach (var inboxMessage in inboxMessages)
                {
                    _logger.LogDebug($"{nameof(ProjectionService)} inbox message payload: {inboxMessage.Payload}");

                    var messagePayload = inboxMessage.Payload;
                    var date = DateHelper.GetDate();

                    switch (inboxMessage.EventType)
                    {
                        case RentalEventType.RentalCreatedEvent:
                            var rentalCreatedEvent = messagePayload.Deseralize<RentalCreatedEvent>();
                            await _rentalWriteRepository.AddAsync(_mapper.Map<Rental>(rentalCreatedEvent));
                            await _rentalInboxRepository.MarkMessageProccessedAsync(rentalCreatedEvent.MessageId, date);
                            break;

                        case RentalEventType.RentalDeletedEvent:
                            var rentalDeletedEvent = messagePayload.Deseralize<RentalDeletedEvent>();
                            await _rentalWriteRepository.DeleteByIdAsync(rentalDeletedEvent.Id);
                            await _rentalInboxRepository.MarkMessageProccessedAsync(rentalDeletedEvent.MessageId, date);
                            break;

                        case RentalEventType.RentalUpdatedEvent:
                            var rentalUpdatedEvent = messagePayload.Deseralize<RentalUpdatedEvent>();
                            await _rentalWriteRepository.UpdateAsync(_mapper.Map<Rental>(rentalUpdatedEvent));
                            await _rentalInboxRepository.MarkMessageProccessedAsync(rentalUpdatedEvent.MessageId, date);
                            break;

                        default:
                            _logger.LogWarning($"{nameof(ProjectionService)} RentalEventType did not match any event");
                            break;
                    }
                }
            }
        }
    }
}
