using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Car;
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

            EfEntity.Brand? brand = null;

            if (carEntity.Brand is not null && await _brandReadRepository.GetByIdAsync(carEntity.Brand.Id) is EfEntity.Brand foundedBrand)
                brand = foundedBrand;

            await _writeRepository.AddAsync(carEntity);
            await _writeRepository.SaveChangesAsync();


            carEntity.Brand = brand;

            var carAddedEvent = _mapper.Map<CarAddedEvent>(carEntity);





            _messageService.SendEventQueue<CarAddedEvent>(
                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CAR_ADDED_ROUTING_KEY,
                @event: carAddedEvent);

            return new CreateCarCommandResponse();
        }
    }
}
