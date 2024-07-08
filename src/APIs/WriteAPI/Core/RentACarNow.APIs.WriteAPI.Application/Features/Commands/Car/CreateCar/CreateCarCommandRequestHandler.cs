using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar
{
    public class CreateCarCommandRequestHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _writeRepository;
        private readonly IEfCoreCarReadRepository _readRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly IValidator<CreateCarCommandRequest> _validator;
        private readonly ILogger<CreateCarCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public CreateCarCommandRequestHandler(
            IEfCoreCarWriteRepository writeRepository,
            IEfCoreCarReadRepository readRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            IValidator<CreateCarCommandRequest> validator,
            ILogger<CreateCarCommandRequestHandler> logger,
            IRabbitMQMessageService messageService,
            IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _validator = validator;
            _logger = logger;
            _messageService = messageService;
            _mapper = mapper;
            _brandReadRepository = brandReadRepository;
        }

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateCarCommandResponse();
            }


            var carEntity = _mapper.Map<EfEntity.Car>(request);

            if (!request.BrandId.Equals(Guid.Empty) &&
                await _brandReadRepository.GetByIdAsync(request.BrandId) is EfEntity.Brand foundedBrand)
            {
                carEntity.Brand = null;

                await _writeRepository.AddAsync(carEntity);
                await _writeRepository.SaveChangesAsync();

                carEntity.Brand = foundedBrand;

            }
            else
            {
                await _writeRepository.AddAsync(carEntity);
                await _writeRepository.SaveChangesAsync();


                var brandAddedEvent = _mapper.Map<BrandAddedEvent>(carEntity.Brand);

                //var carMessage = _mapper.Map<CarMessage>(carEntity);

                //brandAddedEvent.Cars.Add(carMessage); duplicate 

                _messageService.SendEventQueue<BrandAddedEvent>(
                    exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                    routingKey: RabbitMQRoutingKeys.BRAND_ADDED_ROUTING_KEY,
                    @event: brandAddedEvent);


            }

            var carAddedEvent = _mapper.Map<CarAddedEvent>(carEntity);


            _messageService.SendEventQueue<CarAddedEvent>(
                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                    routingKey: RabbitMQRoutingKeys.CAR_ADDED_ROUTING_KEY,
                    @event: carAddedEvent);

            return new CreateCarCommandResponse();
        }
    }
}
