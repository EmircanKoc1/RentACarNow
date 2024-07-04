using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.CreateCustomer;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.DeleteCustomer;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Customer;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandRequestHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        private readonly IEfCoreCustomerWriteRepository _writeRepository;
        private readonly IEfCoreCustomerReadRepository _readRepository;
        private readonly IValidator<UpdateCustomerCommandRequest> _validator;
        private readonly ILogger<UpdateCustomerCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandRequestHandler(
            IEfCoreCustomerWriteRepository writeRepository,
            IEfCoreCustomerReadRepository readRepository,
            IValidator<UpdateCustomerCommandRequest> validator,
            ILogger<UpdateCustomerCommandRequestHandler> logger,
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

        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateCustomerCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateCustomerCommandResponse { };


            var customerEntity = _mapper.Map<EfEntity.Customer>(request);


            await _writeRepository.UpdateAsync(customerEntity);
            await _writeRepository.SaveChangesAsync();

            var customerUpdatedEvent = _mapper.Map<CustomerUpdatedEvent>(customerEntity);

            _messageService.SendEventQueue<CustomerUpdatedEvent>(
                exchangeName: RabbitMQExchanges.CUSTOMER_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CUSTOMER_UPDATED_ROUTING_KEY,
                @event: customerUpdatedEvent);


            return new UpdateCustomerCommandResponse { };
        }
    }

}
