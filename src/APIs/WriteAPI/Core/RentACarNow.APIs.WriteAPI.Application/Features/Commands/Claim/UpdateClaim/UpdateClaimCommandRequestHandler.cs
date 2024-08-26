using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Events.Claim;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.UpdateClaim
{
    public class UpdateClaimCommandRequestHandler : IRequestHandler<UpdateClaimCommandRequest, UpdateClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly IEfCoreClaimReadRepository _readRepository;
        private readonly IValidator<UpdateClaimCommandRequest> _validator;
        private readonly ILogger<UpdateClaimCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        public UpdateClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository writeRepository,
            IEfCoreClaimReadRepository readRepository,
            IValidator<UpdateClaimCommandRequest> validator,
            ILogger<UpdateClaimCommandRequestHandler> logger,
            IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UpdateClaimCommandResponse> Handle(UpdateClaimCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateClaimCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateClaimCommandResponse { };


            var claimEntity = _mapper.Map<EfEntity.Claim>(request);


            await _writeRepository.UpdateAsync(claimEntity);
            await _writeRepository.SaveChangesAsync();

            var claimUpdatedEvent = _mapper.Map<ClaimUpdatedEvent>(claimEntity);


            return new UpdateClaimCommandResponse { };
        }
    }

}
