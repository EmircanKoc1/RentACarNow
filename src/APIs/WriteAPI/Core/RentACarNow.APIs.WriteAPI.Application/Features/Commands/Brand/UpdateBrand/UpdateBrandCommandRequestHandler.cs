using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand
{
    public class UpdateBrandCommandRequestHandler : IRequestHandler<UpdateBrandCommandRequest, UpdateBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _writeRepository;
        private readonly IEfCoreBrandReadRepository _readRepository;
        private readonly IValidator<UpdateBrandCommandRequest> _validator;
        private readonly ILogger<UpdateBrandCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public UpdateBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository writeRepository,
            IEfCoreBrandReadRepository readRepository,
            IValidator<UpdateBrandCommandRequest> validator,
            ILogger<UpdateBrandCommandRequestHandler> logger,
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

        public async Task<UpdateBrandCommandResponse> Handle(UpdateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateBrandCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateBrandCommandResponse { };


            var brandEntity = _mapper.Map<EfEntity.Brand>(request);


            await _writeRepository.UpdateAsync(brandEntity);
            await _writeRepository.SaveChangesAsync();

            var brandUpdatedEvent = _mapper.Map<BrandUpdatedEvent>(brandEntity);

            _messageService.SendEventQueue<BrandUpdatedEvent>(
                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.BRAND_UPDATED_ROUTING_KEY,
                @event: brandUpdatedEvent);


            return new UpdateBrandCommandResponse { };
        }
    }

}
