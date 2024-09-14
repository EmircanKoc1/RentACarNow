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
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.UpdateUser
{
    public class UpdateUserCommandRequestHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {

        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<UpdateUserCommandRequestHandler> _logger;
        private readonly IValidator<UpdateUserCommandRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;
        private readonly IUserEventFactory _userEventFactory;

        public UpdateUserCommandRequestHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<UpdateUserCommandRequestHandler> logger,
            IValidator<UpdateUserCommandRequest> validator,
            IMapper mapper,
            IDateService dateService,
            IGuidService guidService,
            IUserEventFactory userEventFactory)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _dateService = dateService;
            _guidService = guidService;
            _userEventFactory = userEventFactory;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {

            _logger.LogDebug($"{nameof(UpdateUserCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(UpdateUserCommandRequestHandler)} Request not validated");

                return new UpdateUserCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };

            }
            var userExists = await _userReadRepository.IsExistsAsync(request.UserId);

            if (!userExists)
            {
                _logger.LogInformation($"{nameof(UpdateUserCommandRequestHandler)} Entity not found , id : {request.UserId}");

                return new UpdateUserCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "user not found",
                            PropertyName = null
                        }
}
                };


            }


            var generatedUpdatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();



            var efEntity = _mapper.Map<EfEntity.User>(request);
            efEntity.UpdatedDate = generatedUpdatedDate;


            var userUpdatedEvent = _userEventFactory.CreateUserUpdatedEvent(
               userId: request.UserId,
               name: request.Name,
               surname: request.Surname,
               age: request.Age,
               phoneNumber: request.PhoneNumber,
               email: request.Email,
               password: request.Password,
               walletBalance: request.WalletBalance,
               updatedDate: generatedUpdatedDate).SetMessageId<UserUpdatedEvent>(generatedMessageId);


            using var efTran = await _userWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _userOutboxRepository.StartSessionAsync();



            try
            {
                mongoSession.StartTransaction();

                await _userWriteRepository.UpdateAsync(efEntity);
                await _userWriteRepository.SaveChangesAsync();

                var outboxMessage = new UserOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    EventType = UserEventType.UserUpdatedEvent,
                    Payload = userUpdatedEvent.Serialize()!
                };

                await _userOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();

                _logger.LogInformation($"{nameof(UpdateUserCommandRequestHandler)} Transaction commited");

            }
            catch (Exception)
            {

                await mongoSession.AbortTransactionAsync();
                await efTran.CommitAsync();

                _logger.LogError($"{nameof(UpdateUserCommandRequestHandler)} transaction rollbacked");

                return new UpdateUserCommandResponse
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

            return new UpdateUserCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };



        }
    }

}
