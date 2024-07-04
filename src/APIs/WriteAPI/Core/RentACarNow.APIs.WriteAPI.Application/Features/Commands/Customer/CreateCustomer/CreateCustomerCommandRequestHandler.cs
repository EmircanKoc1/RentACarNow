using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Customer; // Assuming Customer events namespace
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandRequestHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        private readonly IEfCoreCustomerWriteRepository _writeRepository;
        private readonly IEfCoreCustomerReadRepository _readRepository;
        private readonly IValidator<CreateCustomerCommandRequest> _validator;
        private readonly ILogger<CreateCustomerCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public CreateCustomerCommandRequestHandler(
            IEfCoreCustomerWriteRepository writeRepository,
            IEfCoreCustomerReadRepository readRepository,
            IValidator<CreateCustomerCommandRequest> validator,
            ILogger<CreateCustomerCommandRequestHandler> logger,
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

        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateCustomerCommandResponse();
            }

            var customerEntity = _mapper.Map<EfEntity.Customer>(request);

            await _writeRepository.AddAsync(customerEntity);
            await _writeRepository.SaveChangesAsync();

            var customerAddedEvent = _mapper.Map<CustomerAddedEvent>(customerEntity);

            _messageService.SendEventQueue<CustomerAddedEvent>(
                exchangeName: RabbitMQExchanges.CUSTOMER_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CUSTOMER_ADDED_ROUTING_KEY,
                @event: customerAddedEvent);

            return new CreateCustomerCommandResponse();
        }
    }
}
