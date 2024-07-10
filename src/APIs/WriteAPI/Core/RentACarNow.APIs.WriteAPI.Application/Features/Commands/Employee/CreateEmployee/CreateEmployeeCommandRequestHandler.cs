using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Events.Employee;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.CreateEmployee
{
    public class CreateEmployeeCommandRequestHandler : IRequestHandler<CreateEmployeeCommandRequest, CreateEmployeeCommandResponse>
    {
        private readonly IEfCoreEmployeeWriteRepository _writeRepository;
        private readonly IEfCoreEmployeeReadRepository _readRepository;
        private readonly IValidator<CreateEmployeeCommandRequest> _validator;
        private readonly ILogger<CreateEmployeeCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandRequestHandler(
            IEfCoreEmployeeWriteRepository writeRepository,
            IEfCoreEmployeeReadRepository readRepository,
            IValidator<CreateEmployeeCommandRequest> validator,
            ILogger<CreateEmployeeCommandRequestHandler> logger,
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

        public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateEmployeeCommandResponse();
            }

            var employeeEntity = _mapper.Map<EfEntity.Employee>(request);

            await _writeRepository.AddAsync(employeeEntity);
            await _writeRepository.SaveChangesAsync();

            var employeeAddedEvent = _mapper.Map<EmployeeAddedEvent>(employeeEntity);

            _messageService.SendEventQueue<EmployeeAddedEvent>(
                exchangeName: RabbitMQExchanges.EMPLOYEE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.EMPLOYEE_ADDED_ROUTING_KEY,
                @event: employeeAddedEvent);

            employeeAddedEvent.Claims?.ToList().ForEach(cm =>
            {

                _messageService.SendEventQueue<ClaimAddedToEmployeeEvent>(
                    exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                    routingKey: RabbitMQRoutingKeys.CLAIM_ADDED_TO_EMPLOYEE_ROUTING_KEY,
                    @event: new ClaimAddedToEmployeeEvent
                    {
                        ClaimId = cm.Id,
                        EmployeeId = employeeAddedEvent.Id,
                        Key = cm.Key,
                        Value = cm.Value,
                        CreatedDate = DateTime.Now,
                        DeletedDate = null,
                        UpdatedDate = null
                    });

                _messageService.SendEventQueue<ClaimAddedEvent>(
                    exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                    routingKey: RabbitMQRoutingKeys.CLAIM_ADDED_ROUTING_KEY,
                    @event: new ClaimAddedEvent
                    {
                        Id = cm.Id,
                        Key = cm.Key,
                        Value = cm.Value,
                        CreatedDate = DateTime.Now,
                        DeletedDate = null,
                        UpdatedDate = null

                    });

            });




            return new CreateEmployeeCommandResponse();
        }
    }
}
