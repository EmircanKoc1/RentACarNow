using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimDeletedUser
{

    public class ClaimDeleteUserCommandRequestHandler : IRequestHandler<ClaimDeleteUserCommandRequest, ClaimDeleteUserCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IEfCoreClaimReadRepository _claimReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<ClaimDeleteUserCommandRequestHandler> _logger;
        private readonly IValidator<ClaimDeleteUserCommandRequest> _validator;
        private readonly IMapper _mapper;

        public ClaimDeleteUserCommandRequestHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IEfCoreClaimReadRepository claimReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<ClaimDeleteUserCommandRequestHandler> logger,
            IValidator<ClaimDeleteUserCommandRequest> validator,
            IMapper mapper)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _claimReadRepository = claimReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ClaimDeleteUserCommandResponse> Handle(ClaimDeleteUserCommandRequest request, CancellationToken cancellationToken)
        {

            _logger.LogDebug($"{nameof(ClaimDeleteUserCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(ClaimDeleteUserCommandRequestHandler)} Request not validated");


                return new ClaimDeleteUserCommandResponse
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

                _logger.LogInformation($"{nameof(ClaimDeleteUserCommandRequestHandler)} Request not validated");


                return new ClaimDeleteUserCommandResponse
                {
                    UserId = request.UserId,
                    ClaimId = request.ClaimId,
                    StatusCode = HttpStatusCode.NotFound,
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



            var userClaimDeletedEvent = new UserClaimDeletedEvent
            {
                MessageId = Guid.NewGuid(),
                DeletedDate = DateHelper.GetDate(),
                UserId = request.UserId,
                Claim = _mapper.Map<ClaimMessage>(efClaim)
            };


            using var efTran = await _userWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _userOutboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _userWriteRepository.DeleteClaimToUser(request.UserId, request.ClaimId);
                await _userWriteRepository.SaveChangesAsync();

                var outboxMessage = new UserOutboxMessage
                {
                    Id = userClaimDeletedEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
                    EventType = UserEventType.UserClaimDeletedEvent,
                    Payload = userClaimDeletedEvent.Serialize()!
                };

                await _userOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();
            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTran.RollbackAsync();

                _logger.LogError($"{nameof(ClaimAddUserCommandRequestHandler)} transaction rollbacked");

                return new ClaimDeleteUserCommandResponse
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

            return new ClaimDeleteUserCommandResponse
            {
                UserId = request.UserId,
                ClaimId = request.ClaimId,
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };


        }
    }

}
