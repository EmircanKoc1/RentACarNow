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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {

        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IValidator<UpdateUserCommandRequest> _validator;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<UpdateUserCommandHandler> logger,
            IValidator<UpdateUserCommandRequest> validator,
            IMapper mapper)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateUserCommandResponse();
            }

            var efEntity = _mapper.Map<EfEntity.User>(request);


            var userUpdatedEvent = _mapper.Map<UserUpdatedEvent>(efEntity);

            using var efTran = await _userWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _userOutboxRepository.StartSessionAsync();



            try
            {
                mongoSession.StartTransaction();

                await _userWriteRepository.UpdateAsync(efEntity);
                await _userWriteRepository.SaveChangesAsync();
                await _userOutboxRepository.AddMessageAsync(new UserOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    EventType = UserEventType.UserUpdatedEvent,
                    Payload = userUpdatedEvent.Serialize()!
                }, mongoSession);



                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();
            }
            catch (Exception)
            {

                await mongoSession.AbortTransactionAsync();
                await efTran.CommitAsync();
                throw;
            }

            return null;



        }
    }

}
