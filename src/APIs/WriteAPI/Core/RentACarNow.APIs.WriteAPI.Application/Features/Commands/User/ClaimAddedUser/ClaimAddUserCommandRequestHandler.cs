using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser
{

    public class ClaimAddUserCommandRequestHandler : IRequestHandler<ClaimAddUserCommandRequest, ClaimAddUserCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IEfCoreClaimReadRepository _claimReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<ClaimAddUserCommandRequestHandler> _logger;
        private readonly IValidator<ClaimAddUserCommandRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IUserEventFactory _userEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public ClaimAddUserCommandRequestHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IEfCoreClaimReadRepository claimReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<ClaimAddUserCommandRequestHandler> logger,
            IValidator<ClaimAddUserCommandRequest> validator,
            IMapper mapper,
            IUserEventFactory userEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _claimReadRepository = claimReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _userEventFactory = userEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<ClaimAddUserCommandResponse> Handle(ClaimAddUserCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(ClaimAddUserCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(ClaimAddUserCommandRequestHandler)} Request not validated");


                return new ClaimAddUserCommandResponse
                {
                    UserId = request.UserId,
                    ClaimId = request.ClaimId,
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var efClaim = await _claimReadRepository.GetByIdAsync(request.ClaimId);
            var userIsExists = await _userReadRepository.IsExistsAsync(request.UserId);

            if (!userIsExists || efClaim is null)
            {

                _logger.LogInformation($"{nameof(ClaimAddUserCommandRequestHandler)} Request not validated");


                return new ClaimAddUserCommandResponse
                {
                    UserId = request.UserId,
                    ClaimId = request.ClaimId,
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "user or claim not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var generatedUpdatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();

            var userClaimAddedEvent = _userEventFactory.CreateUserClaimAddedEvent(
                userId: request.UserId,
                claimId: request.ClaimId,
                key: efClaim.Key,
                value: efClaim.Value).SetMessageId<UserClaimAddedEvent>(generatedMessageId);


            using var efTran = await _userWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _userOutboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _userWriteRepository.AddClaimToUser(request.UserId, request.ClaimId);
                await _userWriteRepository.SaveChangesAsync();


                var outboxMessage = new UserOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    EventType = UserEventType.UserClaimAddedEvent,
                    Payload = userClaimAddedEvent.Serialize()!
                };

                await _userOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();
            }
            catch
            {
                await mongoSession.AbortTransactionAsync();
                await efTran.RollbackAsync();

                _logger.LogError($"{nameof(ClaimAddUserCommandRequestHandler)} transaction rollbacked");

                return new ClaimAddUserCommandResponse
                {

                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "Transaction exception",
                            PropertyName = null
                        }
                    }
                };
            }


            return new ClaimAddUserCommandResponse
            {
                UserId = request.UserId,
                ClaimId = request.ClaimId,
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };

        }


    }

}
