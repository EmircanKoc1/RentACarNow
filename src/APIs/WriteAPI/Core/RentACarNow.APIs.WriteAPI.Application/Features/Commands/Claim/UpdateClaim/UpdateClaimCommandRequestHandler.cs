using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Models;
using System.Net;
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
            _logger.LogDebug($"{nameof(UpdateClaimCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(UpdateClaimCommandRequestHandler)} Request not validated");

                return new UpdateClaimCommandResponse
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
                _logger.LogInformation($"{nameof(UpdateClaimCommandRequestHandler)} Entity not found , id : {request.ClaimId}");

                return new UpdateClaimCommandResponse
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

            var claimEntity = _mapper.Map<EfEntity.Claim>(request);
            claimEntity.UpdatedDate = DateHelper.GetDate();

            var claimUpdatedEvent = _mapper.Map<ClaimUpdatedEvent>(claimEntity);
            claimUpdatedEvent.MessageId = Guid.NewGuid();

            using var efTran = await _writeRepository.BeginTransactionAsync();
            using var mongoSession = await _outboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _writeRepository.UpdateAsync(claimEntity);
                await _writeRepository.SaveChangesAsync();


                var outboxMessage = new ClaimOutboxMessage
                {
                    AddedDate = DateHelper.GetDate(),
                    ClaimEventType = ClaimEventType.ClaimUpdatedEvent,
                    Id = claimUpdatedEvent.MessageId,
                    Payload = claimUpdatedEvent.Serialize()!
                    

                };

                await _outboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();


                _logger.LogInformation($"{nameof(UpdateClaimCommandRequestHandler)} Transaction commited");


            }
            catch (Exception)
            {
                await efTran.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(UpdateClaimCommandRequestHandler)} transaction rollbacked");

                return new UpdateClaimCommandResponse
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



            return new UpdateClaimCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }

}
