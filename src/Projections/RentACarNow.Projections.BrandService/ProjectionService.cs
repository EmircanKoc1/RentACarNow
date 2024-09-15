using AutoMapper;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.BrandService
{
    public class ProjectionService : BackgroundService
    {
        private readonly IBrandInboxRepository _brandInboxRepository;
        private readonly ILogger<ProjectionService> _logger;
        private readonly IMongoBrandWriteRepository _brandWriteRepository;
        private readonly IMapper _mapper;
        private readonly IDateService _dateService;

        public ProjectionService(
            IBrandInboxRepository inboxRepository,
            ILogger<ProjectionService> logger,
            IMongoBrandWriteRepository brandWriteRepository,
            IMapper mapper,
            IDateService dateService)
        {
            _brandInboxRepository = inboxRepository;
            _logger = logger;
            _brandWriteRepository = brandWriteRepository;
            _mapper = mapper;
            _dateService = dateService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ProjectionService)} execute method has been executed ");


            while (!stoppingToken.IsCancellationRequested)
            {
                var inboxMessages = await _brandInboxRepository
               .GetMessagesAsync(5, OrderedDirection.None);

                foreach (var inboxMessage in inboxMessages)
                {
                    _logger.LogDebug($"{nameof(ProjectionService)} inbox message payload : {inboxMessage.Payload}");

                    var messagePayload = inboxMessage.Payload;

                    var date = _dateService.GetDate();

                    switch (inboxMessage.EventType)
                    {
                        case BrandEventType.BrandAddedEvent:
                            var brandCreatedEvent = messagePayload.Deseralize<BrandCreatedEvent>();

                            await _brandWriteRepository.AddAsync(_mapper.Map<Brand>(brandCreatedEvent));
                            await _brandInboxRepository.MarkMessageProccessedAsync(brandCreatedEvent.MessageId, date);

                            break;
                        case BrandEventType.BrandDeletedEvent:
                            var brandDeletedEvent = messagePayload.Deseralize<BrandDeletedEvent>();

                            await _brandWriteRepository.DeleteByIdAsync(brandDeletedEvent.BrandId);
                            await _brandInboxRepository.MarkMessageProccessedAsync(brandDeletedEvent.MessageId, date);

                            break;
                        case BrandEventType.BrandUpdatedEvent:

                            var brandUpdatedEvent = messagePayload.Deseralize<BrandUpdatedEvent>();

                            await _brandWriteRepository.UpdateAsync(_mapper.Map<Brand>(brandUpdatedEvent));
                            await _brandInboxRepository.MarkMessageProccessedAsync(brandUpdatedEvent.MessageId, date);

                            break;
                        default:
                            _logger.LogWarning($"{nameof(ProjectionService)} BrandEventType did not match any event");
                            break;
                    }


                }
            }
           

        }
    }
}
