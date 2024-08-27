using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandRequestHandler : IRequestHandler<CreateClaimCommandRequest, CreateClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly IEfCoreClaimReadRepository _readRepository;
        private readonly IClaimOutboxRepository _claimOutboxRepository;
        private readonly IValidator<CreateClaimCommandRequest> _validator;
        private readonly ILogger<CreateClaimCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public CreateClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository writeRepository,
            IEfCoreClaimReadRepository readRepository,
            IValidator<CreateClaimCommandRequest> validator,
            ILogger<CreateClaimCommandRequestHandler> logger,
            IMapper mapper,
            IClaimOutboxRepository claimOutboxRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _claimOutboxRepository = claimOutboxRepository;
        }

        public async Task<CreateClaimCommandResponse> Handle(CreateClaimCommandRequest request, CancellationToken cancellationToken)
        {


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateClaimCommandResponse();
            }

            var claimEntity = _mapper.Map<EfEntity.Claim>(request);

            claimEntity.Id = Guid.NewGuid();
            var claimAddedEvent = _mapper.Map<ClaimAddedEvent>(claimEntity);

            using var efTransaction = await _writeRepository.BeginTransactionAsync();
            using var mongoSession = await _claimOutboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _writeRepository.AddAsync(claimEntity);
                await _writeRepository.SaveChangesAsync();

                await _claimOutboxRepository.AddMessageAsync(new ClaimOutboxMessage
                {
                    AddedDate = DateTime.UtcNow,
                    ClaimEventType = ClaimEventType.ClaimAddedEvent,
                    Id = Guid.NewGuid(),
                    Payload = claimAddedEvent.Serialize()!
                }, mongoSession);


                await mongoSession.CommitTransactionAsync();
                await efTransaction.CommitAsync();
            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                throw;
            }


            return new CreateClaimCommandResponse();
        }
    }
}
