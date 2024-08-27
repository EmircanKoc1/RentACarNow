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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandRequestHandler : IRequestHandler<DeleteClaimCommandRequest, DeleteClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly IEfCoreClaimReadRepository _readRepository;
        private readonly IClaimOutboxRepository _claimOutboxRepository;
        private readonly IValidator<DeleteClaimCommandRequest> _validator;
        private readonly ILogger<DeleteClaimCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository writeRepository,
            IEfCoreClaimReadRepository readRepository,
            IValidator<DeleteClaimCommandRequest> validator,
            ILogger<DeleteClaimCommandRequestHandler> logger,
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

        public async Task<DeleteClaimCommandResponse> Handle(DeleteClaimCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteClaimCommandResponse();
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                return new DeleteClaimCommandResponse();
            }

            var claimEntity = _mapper.Map<EfEntity.Claim>(request);

            var claimDeletedEvent = _mapper.Map<ClaimDeletedEvent>(claimEntity);

            using var efTran = _writeRepository.BeginTransaction();
            using var mongoSession = await _claimOutboxRepository.StartSessionAsync();

            try
            {
                mongoSession.StartTransaction();


                _writeRepository.Delete(claimEntity);
                _writeRepository.SaveChanges();

                await _claimOutboxRepository.AddMessageAsync(new ClaimOutboxMessage
                {
                    AddedDate = DateTime.UtcNow,
                    ClaimEventType = ClaimEventType.ClaimDeletedEvent,
                    Id = Guid.NewGuid(),
                    Payload = claimDeletedEvent.Serialize()!

                }, mongoSession);




                efTran.Commit();
                await mongoSession.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                efTran.Rollback();
                throw;
            }


            return new DeleteClaimCommandResponse();
        }
    }
}
