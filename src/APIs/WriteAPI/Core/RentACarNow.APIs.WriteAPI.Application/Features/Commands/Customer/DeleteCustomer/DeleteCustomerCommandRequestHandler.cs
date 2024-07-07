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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.DeleteCustomer
{
    public class DeleteCustomerCommandRequestHandler : IRequestHandler<DeleteCustomerCommandRequest, DeleteCustomerCommandResponse>
    {
        private readonly IEfCoreCustomerWriteRepository _writeRepository;
        private readonly IEfCoreCustomerReadRepository _readRepository;
        private readonly IValidator<DeleteCustomerCommandRequest> _validator;
        private readonly ILogger<DeleteCustomerCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandRequestHandler(
            IEfCoreCustomerWriteRepository writeRepository,
            IEfCoreCustomerReadRepository readRepository,
            IValidator<DeleteCustomerCommandRequest> validator,
            ILogger<DeleteCustomerCommandRequestHandler> logger,
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

        public async Task<DeleteCustomerCommandResponse> Handle(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteCustomerCommandResponse();
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                return new DeleteCustomerCommandResponse();
            }

            var customerEntity = _mapper.Map<EfEntity.Customer>(request);

            _writeRepository.Delete(customerEntity);
            _writeRepository.SaveChanges();

            var customerDeletedEvent = _mapper.Map<CustomerDeletedEvent>(customerEntity);

            _messageService.SendEventQueue<CustomerDeletedEvent>(
                exchangeName: RabbitMQExchanges.CUSTOMER_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CUSTOMER_DELETED_ROUTING_KEY,
                @event: customerDeletedEvent);

            return new DeleteCustomerCommandResponse();
        }
    }
}
