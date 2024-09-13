using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandRequestHandler : IRequestHandler<DeleteClaimCommandRequest, DeleteClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly IEfCoreClaimReadRepository _readRepository;
        private readonly IClaimOutboxRepository _claimOutboxRepository;
        private readonly IValidator<DeleteClaimCommandRequest> _validator;
        private readonly ILogger<DeleteClaimCommandRequestHandler> _logger;
        private readonly IClaimEventFactory _claimEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public DeleteClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository writeRepository,
            IEfCoreClaimReadRepository readRepository,
            IClaimOutboxRepository claimOutboxRepository,
            IValidator<DeleteClaimCommandRequest> validator,
            ILogger<DeleteClaimCommandRequestHandler> logger,
            IClaimEventFactory claimEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _claimOutboxRepository = claimOutboxRepository;
            _validator = validator;
            _logger = logger;
            _claimEventFactory = claimEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<DeleteClaimCommandResponse> Handle(DeleteClaimCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(DeleteClaimCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(DeleteClaimCommandRequestHandler)} Request not validated");


                return new DeleteClaimCommandResponse
                {

                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var isExists = await _readRepository.IsExistsAsync(request.ClaimId);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(DeleteClaimCommandRequestHandler)} claim not found , id : {request.ClaimId}");
                return new DeleteClaimCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "claim not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var generatedDeletedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();


            var claimDeletedEvent = _claimEventFactory.CreateClaimDeletedEvent(
                claimId: request.ClaimId,
                deletedDate: generatedDeletedDate).SetMessageId<ClaimDeletedEvent>(generatedMessageId);

        
            using var efTran = _writeRepository.BeginTransaction();
            using var mongoSession = await _claimOutboxRepository.StartSessionAsync();

            try
            {
                mongoSession.StartTransaction();


                _writeRepository.DeleteById(request.ClaimId);
                await _writeRepository.SaveChangesAsync();


                var outboxMessage = new ClaimOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    ClaimEventType = ClaimEventType.ClaimDeletedEvent,
                    Payload = claimDeletedEvent.Serialize()!

                };

                await _claimOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await efTran.CommitAsync();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(DeleteClaimCommandRequestHandler)} Transaction commited");
            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTran.RollbackAsync();

                _logger.LogError($"{nameof(DeleteClaimCommandRequestHandler)} transaction rollbacked");

                return new DeleteClaimCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "Transaction exception",
                            PropertyName = null
                        }
                    }
                };
            }


            return new DeleteClaimCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
