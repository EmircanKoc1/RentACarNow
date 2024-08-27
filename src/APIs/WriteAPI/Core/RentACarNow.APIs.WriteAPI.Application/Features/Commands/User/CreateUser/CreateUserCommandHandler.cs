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
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, UserCreateCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IValidator<CreateUserCommandRequest> _validator;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<CreateUserCommandHandler> logger,
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

        public async Task<UserCreateCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UserCreateCommandResponse();
            }

            var efUser = _mapper.Map<EfEntity.User>(request);
            efUser.Id = Guid.NewGuid();

            var userCreatedEvent = _mapper.Map<UserCreatedEvent>(efUser);

            using var efTran = await _userWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _userOutboxRepository.StartSessionAsync();

            try
            {
                mongoSession.StartTransaction();

                await _userWriteRepository.AddAsync(efUser);
                await _userWriteRepository.SaveChangesAsync();
                await _userOutboxRepository.AddMessageAsync(new UserOutboxMessage
                {
                    AddedDate = DateTime.UtcNow,
                    EventType = UserEventType.UserCreatedEvent,
                    Id = Guid.NewGuid(),
                    Payload = userCreatedEvent.Serialize()!
                }, mongoSession);




                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();


            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTran.RollbackAsync();
                throw;
            }




            return new UserCreateCommandResponse();

        }
    }

}
