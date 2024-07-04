using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.CreateEmployee;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.DeleteEmployee;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.Common.Interfaces;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Employee;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommandRequestHandler : IRequestHandler<UpdateEmployeeCommandRequest, UpdateEmployeeCommandResponse>
    {
        private readonly IEfCoreEmployeeWriteRepository _writeRepository;
        private readonly IEfCoreEmployeeReadRepository _readRepository;
        private readonly IValidator<UpdateEmployeeCommandRequest> _validator;
        private readonly ILogger<UpdateEmployeeCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;
        public UpdateEmployeeCommandRequestHandler(
            IEfCoreEmployeeWriteRepository writeRepository,
            IEfCoreEmployeeReadRepository readRepository,
            IValidator<UpdateEmployeeCommandRequest> validator,
            ILogger<UpdateEmployeeCommandRequestHandler> logger,
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

        public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateEmployeeCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateEmployeeCommandResponse { };


            var employeeEntity = _mapper.Map<EfEntity.Employee>(request);


            await _writeRepository.UpdateAsync(employeeEntity);
            await _writeRepository.SaveChangesAsync();

            var employeeUpdatedEvent = _mapper.Map<EmployeeUpdatedEvent>(employeeEntity);

            _messageService.SendEventQueue<EmployeeUpdatedEvent>(
                exchangeName: RabbitMQExchanges.EMPLOYEE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.EMPLOYEE_UPDATED_ROUTING_KEY,
                @event: employeeUpdatedEvent);


            return new UpdateEmployeeCommandResponse { };
        }
    }

}
