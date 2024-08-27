using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.UpdateClaim
{
    public class UpdateClaimCommandRequestHandler : IRequestHandler<UpdateClaimCommandRequest, UpdateClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly IEfCoreClaimReadRepository _readRepository;
        private readonly IClaimOutboxRepository _outboxRepository;
        private readonly IValidator<UpdateClaimCommandRequest> _validator;
        private readonly ILogger<UpdateClaimCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        public UpdateClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository writeRepository,
            IEfCoreClaimReadRepository readRepository,
            IValidator<UpdateClaimCommandRequest> validator,
            ILogger<UpdateClaimCommandRequestHandler> logger,
            IMapper mapper,
            IClaimOutboxRepository outboxRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _outboxRepository = outboxRepository;
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

            var claimUpdatedEvent = _mapper.Map<ClaimUpdatedEvent>(claimEntity);

            using var efTran = await _writeRepository.BeginTransactionAsync();
            using var mongoSession = await _outboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _writeRepository.UpdateAsync(claimEntity);
                await _writeRepository.SaveChangesAsync();


                await _outboxRepository.AddMessageAsync(new Common.Entities.OutboxEntities.ClaimOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    ClaimEventType = ClaimEventType.ClaimUpdatedEvent,
                    Id = Guid.NewGuid(),
                    Payload = claimUpdatedEvent.Serialize()!


                }, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();

            }
            catch (Exception)
            {
                await efTran.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                throw;
            }


            return new UpdateClaimCommandResponse { };
        }
    }

}
