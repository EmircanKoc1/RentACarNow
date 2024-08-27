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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IValidator<DeleteUserCommandRequest> _validator;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<DeleteUserCommandHandler> logger,
            IValidator<DeleteUserCommandRequest> validator,
            IMapper mapper)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteUserCommandResponse();
            }

            var efUser = _mapper.Map<EfEntity.User>(request);


            var userDeletedEvent = _mapper.Map<UserCreatedEvent>(efUser);

            using var mongoSession = await _userOutboxRepository.StartSessionAsync();
            using var efTran = _userWriteRepository.BeginTransaction();


            try
            {
                mongoSession.StartTransaction();

                await _userOutboxRepository.AddMessageAsync(new UserOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    EventType = UserEventType.UserDeletedEvent,
                    Payload = userDeletedEvent.Serialize()!,
                    Id = Guid.NewGuid(),

                }, mongoSession);

                _userWriteRepository.Delete(efUser);
                _userWriteRepository.SaveChanges();


                await mongoSession.CommitTransactionAsync();
                efTran.Commit();

            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                efTran.Rollback();

                throw;
            }

            return null;
        }
    }

}
