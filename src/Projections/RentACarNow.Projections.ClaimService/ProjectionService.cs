using AutoMapper;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoEntities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using RentACarNow.Common.Enums.RepositoryEnums;

namespace RentACarNow.Projections.ClaimService
{
    public class ProjectionService : BackgroundService
    {
        private readonly IClaimnboxRepository _claimInboxRepository;
        private readonly ILogger<ProjectionService> _logger;
        private readonly IMongoClaimWriteRepository _claimWriteRepository;
        private readonly IMapper _mapper;

        public ProjectionService(
            IClaimnboxRepository claimInboxRepository,
            ILogger<ProjectionService> logger,
            IMongoClaimWriteRepository claimWriteRepository,
            IMapper mapper)
        {
            _claimInboxRepository = claimInboxRepository;
            _logger = logger;
            _claimWriteRepository = claimWriteRepository;
            _mapper = mapper;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ProjectionService)} execute method has been executed ");

            while (!stoppingToken.IsCancellationRequested)
            {
                var inboxMessages = await _claimInboxRepository
                   .GetMessagesAsync(5, OrderedDirection.None);

                foreach (var inboxMessage in inboxMessages)
                {
                    _logger.LogDebug($"{nameof(ProjectionService)} inbox message payload : {inboxMessage.Payload}");

                    var messagePayload = inboxMessage.Payload;
                    var date = DateHelper.GetDate();

                    switch (inboxMessage.EventType)
                    {
                        case ClaimEventType.ClaimAddedEvent:
                            var claimAddedEvent = messagePayload.Deseralize<ClaimAddedEvent>();
                            await _claimWriteRepository.AddAsync(_mapper.Map<Claim>(claimAddedEvent));
                            await _claimInboxRepository.MarkMessageProccessedAsync(claimAddedEvent.MessageId, date);
                            break;

                        case ClaimEventType.ClaimDeletedEvent:
                            var claimDeletedEvent = messagePayload.Deseralize<ClaimDeletedEvent>();
                            await _claimWriteRepository.DeleteByIdAsync(claimDeletedEvent.Id);
                            await _claimInboxRepository.MarkMessageProccessedAsync(claimDeletedEvent.MessageId, date);
                            break;

                        case ClaimEventType.ClaimUpdatedEvent:
                            var claimUpdatedEvent = messagePayload.Deseralize<ClaimUpdatedEvent>();
                            await _claimWriteRepository.UpdateAsync(_mapper.Map<Claim>(claimUpdatedEvent));
                            await _claimInboxRepository.MarkMessageProccessedAsync(claimUpdatedEvent.MessageId, date);
                            break;

                        default:
                            _logger.LogWarning($"{nameof(ProjectionService)} ClaimEventType did not match any event");
                            break;
                    }
                }
            }
        }
    }
}
