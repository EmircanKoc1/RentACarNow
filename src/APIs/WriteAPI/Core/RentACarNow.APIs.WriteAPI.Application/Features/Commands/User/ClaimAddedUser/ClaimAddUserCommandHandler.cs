using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser
{

    public class ClaimAddUserCommandHandler : IRequestHandler<ClaimAddUserCommandRequest, ClaimAddUserCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IEfCoreClaimReadRepository _claimReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<ClaimAddUserCommandHandler> _logger;
        private readonly IValidator<ClaimAddUserCommandRequest> _validator;
        private readonly IMapper _mapper;

        public ClaimAddUserCommandHandler(
            IEfCoreUserWriteRepository userWriteRepository,
            IEfCoreUserReadRepository userReadRepository,
            IEfCoreClaimReadRepository claimReadRepository,
            IUserOutboxRepository userOutboxRepository,
            ILogger<ClaimAddUserCommandHandler> logger,
            IValidator<ClaimAddUserCommandRequest> validator,
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

        public async Task<ClaimAddUserCommandResponse> Handle(ClaimAddUserCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new ClaimAddUserCommandResponse();
            }

            var efClaim = await _claimReadRepository.GetByIdAsync(request.ClaimId);
            var userIsExists = await _userReadRepository.IsExistsAsync(request.UserId);

            if (efClaim is null || !userIsExists)
                return null;


            using var efTran = await _userWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _userOutboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _userWriteRepository.AddClaimToUser(request.UserId, request.ClaimId);
                await _userWriteRepository.SaveChangesAsync();

                await _userOutboxRepository.AddMessageAsync(new UserOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    EventType = UserEventType.UserClaimAddedEvent,
                    Id = Guid.NewGuid(),
                    Payload = new UserClaimAddedEvent
                    {
                        UserId = request.UserId,
                        Claim = _mapper.Map<ClaimMessage>(efClaim)
                    }.Serialize()!
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

            return null;

        }


    }

}
