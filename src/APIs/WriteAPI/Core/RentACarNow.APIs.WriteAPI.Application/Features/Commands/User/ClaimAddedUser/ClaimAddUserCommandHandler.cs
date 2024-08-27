using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.User.ClaimAddedUser
{

    public class ClaimAddUserCommandHandler : IRequestHandler<ClaimAddUserCommandRequest, ClaimAddUserCommandResponse>
    {
        private readonly IEfCoreUserWriteRepository _userWriteRepository;
        private readonly IEfCoreUserReadRepository _userReadRepository;
        private readonly IEfCoreClaimReadRepository _claimReadRepository;
        private readonly IEfCoreClaimWriteRepository _claimWriteRepository;
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
            IMapper mapper,
            IEfCoreClaimWriteRepository claimWriteRepository)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userOutboxRepository = userOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _claimWriteRepository = claimWriteRepository;
        }

        public async Task<ClaimAddUserCommandResponse> Handle(ClaimAddUserCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new ClaimAddUserCommandResponse();
            }

            var efClaim = await _claimReadRepository.GetByIdAsync(request.ClaimId);
            var efUser = await _userReadRepository.GetByIdAsync(request.UserId,true);

            if (efClaim is null || efUser is null)
                return null;

            efUser.Claims.Add(efClaim);

            await _userWriteRepository.SaveChangesAsync();

            return null;

        }


    }

}
