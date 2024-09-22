using AutoMapper;
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
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandRequestHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<CreateUserCommandRequestHandler> _logger;
        private readonly IValidator<CreateUserCommandRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IUserEventFactory _userEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public CreateUserCommandRequestHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<CreateUserCommandRequestHandler> logger,
            IValidator<CreateUserCommandRequest> validator,
            IMapper mapper,
            IUserEventFactory userEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _userEventFactory = userEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            _logger.LogDebug($"{nameof(CreateUserCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(CreateUserCommandRequestHandler)} Request not validated");


                return new CreateUserCommandResponse
                {
                    UserId = _guidService.GetEmptyGuid(),
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }


            var generatedCreatedDate = _dateService.GetDate();
            var generatedEntityId = _guidService.CreateGuid();
            var generatedMessageId = _guidService.CreateGuid();
            var generatedMessageAddedDate = _dateService.GetDate();


            var efEntity = _mapper.Map<EfEntity.User>(request);
            efEntity.CreatedDate = generatedCreatedDate;
            efEntity.Id = generatedEntityId;


            var userCreatedEvent = _userEventFactory.CreateUserCreatedEvent(
                userId: generatedEntityId,
                name: request.Name,
                surname: request.Surname,
                age: request.Age,
                phoneNumber: request.PhoneNumber,
                email: request.Email,
                password: request.Password,
                walletBalance: request.WalletBalance,
                createdDate: generatedCreatedDate).SetMessageId<UserCreatedEvent>(generatedMessageId);

            using var efTran = await _userWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _userOutboxRepository.StartSessionAsync();

            try
            {
                mongoSession.StartTransaction();

                await _userWriteRepository.AddAsync(efEntity);
                await _userWriteRepository.SaveChangesAsync();


                var outboxMessage = new UserOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    EventType = UserEventType.UserCreatedEvent,
                    Payload = userCreatedEvent.Serialize()!
                };

                await _userOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();

                _logger.LogInformation($"{nameof(CreateUserCommandRequestHandler)} Transaction commited");


            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTran.RollbackAsync();
                _logger.LogError($"{nameof(CreateCarCommandRequestHandler)} transaction rollbacked");

                return new CreateUserCommandResponse
                {

                    HttpStatusCode = HttpStatusCode.BadRequest,
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

            return new CreateUserCommandResponse
            {
                UserId = generatedEntityId,
                HttpStatusCode = HttpStatusCode.Created,
                Errors = null
            };


        }



    }

}
