using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequestHandler : IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _writeRepository;
        private readonly IEfCoreBrandReadRepository _readRepository;
        private readonly IValidator<CreateBrandCommandRequest> _validator;
        private readonly ILogger<CreateBrandCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public CreateBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository writeRepository,
            IEfCoreBrandReadRepository readRepository,
            IValidator<CreateBrandCommandRequest> validator,
            ILogger<CreateBrandCommandRequestHandler> logger,
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

        public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateBrandCommandResponse { };
            }


            var brandEntity = _mapper.Map<EfEntity.Brand>(request);



            await _writeRepository.AddAsync(brandEntity);
            await _writeRepository.SaveChangesAsync();



            var brandAddedEvent = _mapper.Map<BrandAddedEvent>(brandEntity);

            _messageService.SendEventQueue<BrandAddedEvent>(
                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.BRAND_ADDED_ROUTING_KEY,
                @event: brandAddedEvent);


            return new CreateBrandCommandResponse { };
        }
    }

}
