using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand
{
    public class DeleteBrandCommandRequestHandler : IRequestHandler<DeleteBrandCommandRequest, DeleteBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _writeRepository;
        private readonly IEfCoreBrandReadRepository _readRepository;
        private readonly IValidator<DeleteBrandCommandRequest> _validator;
        private readonly ILogger<DeleteBrandCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public DeleteBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository writeRepository,
            IEfCoreBrandReadRepository readRepository,
            IValidator<DeleteBrandCommandRequest> validator,
            ILogger<DeleteBrandCommandRequestHandler> logger,
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

        public async Task<DeleteBrandCommandResponse> Handle(DeleteBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteBrandCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new DeleteBrandCommandResponse { };


            var brandEntity = _mapper.Map<EfEntity.Brand>(request);


             _writeRepository.Delete(brandEntity);
             _writeRepository.SaveChanges();

            var brandDeletedEvent = _mapper.Map<BrandDeletedEvent>(brandEntity);

            _messageService.SendEventQueue<BrandDeletedEvent>(
                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.BRAND_DELETED_ROUTING_KEY,
                @event: brandDeletedEvent);


            return new DeleteBrandCommandResponse { };
        }
    }

}
