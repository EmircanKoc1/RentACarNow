﻿using AutoMapper;
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
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.UpdateClaim
{
    public class UpdateClaimCommandRequestHandler : IRequestHandler<UpdateClaimCommandRequest, UpdateClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _claimWriteRepository;
        private readonly IEfCoreClaimReadRepository _claimReadRepository;
        private readonly IClaimOutboxRepository _outboxRepository;
        private readonly IValidator<UpdateClaimCommandRequest> _validator;
        private readonly ILogger<UpdateClaimCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IClaimEventFactory _claimEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public UpdateClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository claimWriteRepository, 
            IEfCoreClaimReadRepository claimReadRepository, 
            IClaimOutboxRepository outboxRepository, 
            IValidator<UpdateClaimCommandRequest> validator,
            ILogger<UpdateClaimCommandRequestHandler> logger,
            IMapper mapper, 
            IClaimEventFactory claimEventFactory, 
            IDateService dateService, 
            IGuidService guidService)
        {
            _claimWriteRepository = claimWriteRepository;
            _claimReadRepository = claimReadRepository;
            _outboxRepository = outboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _claimEventFactory = claimEventFactory;
            _dateService = dateService;
            _guidService = guidService;
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
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };

            }

            var isExists = await _claimReadRepository.IsExistsAsync(request.ClaimId);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(UpdateClaimCommandRequestHandler)} Entity not found , id : {request.ClaimId}");

                return new UpdateClaimCommandResponse
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
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

            var generatedUpdatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate(); 
            var generatedMessageId = _guidService.CreateGuid();



            var efEntity = _mapper.Map<EfEntity.Claim>(request);
            efEntity.UpdatedDate = generatedUpdatedDate;

            var claimUpdatedEvent = _claimEventFactory.CreateClaimUpdatedEvent(
                claimId : request.ClaimId,
                key : request.Key,
                value  : request.Value,
                updatedDate : generatedUpdatedDate).SetMessageId<ClaimUpdatedEvent>(generatedMessageId);




            using var efTran = await _claimWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _outboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _claimWriteRepository.UpdateAsync(efEntity);
                await _claimWriteRepository.SaveChangesAsync();


                var outboxMessage = new ClaimOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    ClaimEventType = ClaimEventType.ClaimUpdatedEvent,
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

                    HttpStatusCode = HttpStatusCode.BadRequest,
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
                HttpStatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }

}
