﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Claim; 
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandRequestHandler : IRequestHandler<CreateClaimCommandRequest, CreateClaimCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _writeRepository;
        private readonly IEfCoreClaimReadRepository _readRepository;
        private readonly IValidator<CreateClaimCommandRequest> _validator;
        private readonly ILogger<CreateClaimCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public CreateClaimCommandRequestHandler(
            IEfCoreClaimWriteRepository writeRepository,
            IEfCoreClaimReadRepository readRepository,
            IValidator<CreateClaimCommandRequest> validator,
            ILogger<CreateClaimCommandRequestHandler> logger,
            IRabbitMQMessageService messageService,
            IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _validator = validator;
            _logger = logger;
            _messageService = messageService;
            _mapper = mapper;
        }

        public async Task<CreateClaimCommandResponse> Handle(CreateClaimCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateClaimCommandResponse();
            }

            var claimEntity = _mapper.Map<EfEntity.Claim>(request);

            await _writeRepository.AddAsync(claimEntity);
            await _writeRepository.SaveChangesAsync();

            var claimAddedEvent = _mapper.Map<ClaimAddedEvent>(claimEntity);

            _messageService.SendEventQueue<ClaimAddedEvent>(
                exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CLAIM_ADDED_ROUTING_KEY,
                @event: claimAddedEvent);

            return new CreateClaimCommandResponse();
        }
    }
}
