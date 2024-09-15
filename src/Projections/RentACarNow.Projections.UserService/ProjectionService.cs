using AutoMapper;
using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.UserService
{
    public class ProjectionService : BackgroundService
    {
        private readonly IUserInboxRepository _userInboxRepository;
        private readonly ILogger<ProjectionService> _logger;
        private readonly IMongoUserWriteRepository _userWriteRepository;
        private readonly IMapper _mapper;
        private readonly IDateService _dateService;

        public ProjectionService(
            IUserInboxRepository userInboxRepository,
            ILogger<ProjectionService> logger,
            IMongoUserWriteRepository userWriteRepository,
            IMapper mapper,
            IDateService dateService)
        {
            _userInboxRepository = userInboxRepository;
            _logger = logger;
            _userWriteRepository = userWriteRepository;
            _mapper = mapper;
            _dateService = dateService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ProjectionService)} execute method has been executed");

            while (!stoppingToken.IsCancellationRequested)
            {
                var inboxMessages = await _userInboxRepository
                   .GetMessagesAsync(5, OrderedDirection.None);

                foreach (var inboxMessage in inboxMessages)
                {
                    _logger.LogDebug($"{nameof(ProjectionService)} inbox message payload: {inboxMessage.Payload}");

                    var messagePayload = inboxMessage.Payload;
                    var date = _dateService.GetDate();

                    switch (inboxMessage.EventType)
                    {
                        case UserEventType.UserCreatedEvent:
                            var userCreatedEvent = messagePayload.Deseralize<UserCreatedEvent>();
                            await _userWriteRepository.AddAsync(_mapper.Map<User>(userCreatedEvent));
                            await _userInboxRepository.MarkMessageProccessedAsync(userCreatedEvent.MessageId, date);
                            break;

                        case UserEventType.UserDeletedEvent:
                            var userDeletedEvent = messagePayload.Deseralize<UserDeletedEvent>();
                            await _userWriteRepository.DeleteByIdAsync(userDeletedEvent.UserId);
                            await _userInboxRepository.MarkMessageProccessedAsync(userDeletedEvent.MessageId, date);
                            break;

                        case UserEventType.UserUpdatedEvent:
                            var userUpdatedEvent = messagePayload.Deseralize<UserUpdatedEvent>();
                            await _userWriteRepository.UpdateAsync(_mapper.Map<User>(userUpdatedEvent));
                            await _userInboxRepository.MarkMessageProccessedAsync(userUpdatedEvent.MessageId, date);
                            break;

                        case UserEventType.UserClaimAddedEvent:
                            var userClaimAddedEvent = messagePayload.Deseralize<UserClaimAddedEvent>();


                            var addClaim = _mapper.Map<Claim>(userClaimAddedEvent.Claim);

                            await _userWriteRepository.AddUserClaimAsync(userClaimAddedEvent.UserId, addClaim);
                            await _userInboxRepository.MarkMessageProccessedAsync(userClaimAddedEvent.MessageId, date);
                            break;

                        case UserEventType.UserClaimDeletedEvent:
                            var userClaimDeletedEvent = messagePayload.Deseralize<UserClaimDeletedEvent>();
                            await _userWriteRepository.DeleteUserClaimAsync(userClaimDeletedEvent.UserId, userClaimDeletedEvent.ClaimId);


                            await _userInboxRepository.MarkMessageProccessedAsync(userClaimDeletedEvent.MessageId, date);
                            break;

                        case UserEventType.UserClaimUpdatedEvent:

                            var userClaimUpdatedEvent = messagePayload.Deseralize<UserClaimUpdatedEvent>();
                            
                            var claim = _mapper.Map<Claim>(userClaimUpdatedEvent);

                            await _userWriteRepository.UpdateUserClaimAsync(claim);

                            await _userInboxRepository.MarkMessageProccessedAsync(userClaimUpdatedEvent.MessageId, date);
                            break;

                        default:
                            _logger.LogWarning($"{nameof(ProjectionService)} EventType did not match any event");
                            break;
                    }
                }
            }
        }
    }
}
