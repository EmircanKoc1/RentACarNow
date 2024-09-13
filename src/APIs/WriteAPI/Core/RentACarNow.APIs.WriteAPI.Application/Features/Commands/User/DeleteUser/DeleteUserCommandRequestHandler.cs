using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.DeleteUser
{
    public class DeleteUserCommandRequestHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<DeleteUserCommandRequestHandler> _logger;
        private readonly IValidator<DeleteUserCommandRequest> _validator;
        private readonly IUserEventFactory _userEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public DeleteUserCommandRequestHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<DeleteUserCommandRequestHandler> logger,
            IValidator<DeleteUserCommandRequest> validator,
            IUserEventFactory userEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _userEventFactory = userEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {

            _logger.LogDebug($"{nameof(DeleteUserCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(CreateCarCommandRequestHandler)} Request not validated");


                return new DeleteUserCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var generatedMessageId = _guidService.CreateGuid();
            var generateMessageAddedDate = _dateService.GetDate();
            var generateDeletedDate = _dateService.GetDate();

            var userDeletedEvent = _userEventFactory.CreateUserDeletedEvent(
                userId: request.UserId,
                deletedDate: generateDeletedDate).SetMessageId<UserDeletedEvent>(generatedMessageId);



            using var mongoSession = await _userOutboxRepository.StartSessionAsync();
            using var efTran = await _userWriteRepository.BeginTransactionAsync();


            try
            {
                mongoSession.StartTransaction();

                var outboxMessage = new UserOutboxMessage
                {
                    Id = userDeletedEvent.MessageId,
                    AddedDate = generateMessageAddedDate,
                    EventType = UserEventType.UserDeletedEvent,
                    Payload = userDeletedEvent.Serialize()!,

                };

                await _userOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                _userWriteRepository.DeleteById(request.UserId);
                await _userWriteRepository.SaveChangesAsync();


                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();

                _logger.LogInformation($"{nameof(DeleteUserCommandRequestHandler)} Transaction commited");


            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTran.RollbackAsync();

                _logger.LogError($"{nameof(DeleteUserCommandRequestHandler)} transaction rollbacked");

                return new DeleteUserCommandResponse
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

            return new DeleteUserCommandResponse
            {

                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }

}
