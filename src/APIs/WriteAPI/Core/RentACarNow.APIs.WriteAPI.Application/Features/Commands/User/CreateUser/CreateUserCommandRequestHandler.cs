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
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
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

        public CreateUserCommandRequestHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<CreateUserCommandRequestHandler> logger,
            IValidator<CreateUserCommandRequest> validator,
            IMapper mapper)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
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
                    UserId = Guid.Empty,
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }



            var efUser = _mapper.Map<EfEntity.User>(request);
            efUser.Id = Guid.NewGuid();
            efUser.CreatedDate = DateHelper.GetDate();

            var userCreatedEvent = _mapper.Map<UserCreatedEvent>(efUser);



            using var efTran = await _userWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _userOutboxRepository.StartSessionAsync();

            try
            {
                mongoSession.StartTransaction();

                await _userWriteRepository.AddAsync(efUser);
                await _userWriteRepository.SaveChangesAsync();


                var outboxMessage = new UserOutboxMessage
                {
                    AddedDate = DateHelper.GetDate(),
                    EventType = UserEventType.UserCreatedEvent,
                    Id = userCreatedEvent.MessageId,
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

            return new CreateUserCommandResponse
            {
                UserId = efUser.Id,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };


        }



    }

}
