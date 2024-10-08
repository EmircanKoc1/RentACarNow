﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
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
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandRequestHandler : IRequestHandler<CreateClaimCommandRequest, CreateClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _claimWriteRepository;
        private readonly IEfCoreClaimReadRepository _claimReadRepository;
        private readonly IClaimOutboxRepository _claimOutboxRepository;
        private readonly IValidator<CreateClaimCommandRequest> _validator;
        private readonly ILogger<CreateClaimCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IClaimEventFactory _claimEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public CreateClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository claimWriteRepository,
            IEfCoreClaimReadRepository claimReadRepository,
            IClaimOutboxRepository claimOutboxRepository,
            IValidator<CreateClaimCommandRequest> validator,
            ILogger<CreateClaimCommandRequestHandler> logger,
            IMapper mapper,
            IClaimEventFactory claimEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _claimWriteRepository = claimWriteRepository;
            _claimReadRepository = claimReadRepository;
            _claimOutboxRepository = claimOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _claimEventFactory = claimEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<CreateClaimCommandResponse> Handle(CreateClaimCommandRequest request, CancellationToken cancellationToken)
        {

            _logger.LogDebug($"{nameof(CreateCarCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(CreateCarCommandRequestHandler)} Request not validated");


                return new CreateClaimCommandResponse
                {
                    ClaimId = _guidService.GetEmptyGuid(),
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var generatedEntityId = _guidService.CreateGuid();
            var generatedCreatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();


            var claimEntity = _mapper.Map<EfEntity.Claim>(request);
            claimEntity.Id = generatedEntityId;
            claimEntity.CreatedDate = generatedCreatedDate;

            var claimCreatedEvent = _claimEventFactory.CreateClaimCreatedEvent(
                claimId: generatedEntityId,
                key: request.Key,
                value: request.Value,
                createdDate: generatedCreatedDate).SetMessageId<ClaimCreatedEvent>(generatedMessageId);

            using var efTransaction = await _claimWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _claimOutboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _claimWriteRepository.AddAsync(claimEntity);
                await _claimWriteRepository.SaveChangesAsync();

                var outboxMessage = new ClaimOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    ClaimEventType = ClaimEventType.ClaimAddedEvent,
                    Payload = claimCreatedEvent.Serialize()!
                };

                await _claimOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);


                await mongoSession.CommitTransactionAsync();
                await efTransaction.CommitAsync();

                _logger.LogInformation($"{nameof(CreateClaimCommandRequestHandler)} Transaction commited");

            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();
                _logger.LogError($"{nameof(CreateClaimCommandRequestHandler)} transaction rollbacked");

                return new CreateClaimCommandResponse
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


            return new CreateClaimCommandResponse
            {
                ClaimId = generatedEntityId,
                HttpStatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }
}
