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
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimDeletedUser
{

    public class ClaimDeleteUserCommandHandler : IRequestHandler<ClaimDeleteUserCommandRequest, ClaimDeleteUserCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IEfCoreClaimReadRepository _claimReadRepository;
        private readonly IUserOutboxRepository _userOutboxRepository;
        private readonly ILogger<ClaimDeleteUserCommandHandler> _logger;
        private readonly IValidator<ClaimDeleteUserCommandRequest> _validator;
        private readonly IMapper _mapper;

        public ClaimDeleteUserCommandHandler(
            IEfCoreUserWriteRepository userWriteRepository, 
            IEfCoreUserReadRepository userReadRepository, 
            IEfCoreClaimReadRepository claimReadRepository, 
            IUserOutboxRepository userOutboxRepository, 
            ILogger<ClaimDeleteUserCommandHandler> logger, 
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

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new ClaimDeleteUserCommandResponse();
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

                await _userWriteRepository.DeleteClaimToUser(request.UserId, request.ClaimId);
                await _userWriteRepository.SaveChangesAsync();

                await _userOutboxRepository.AddMessageAsync(new UserOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    EventType = UserEventType.UserClaimDeletedEvent,
                    Id = Guid.NewGuid(),
                    Payload = new UserClaimDeletedEvent
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
