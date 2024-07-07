using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Employee; 
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.DeleteEmployee
{
    public class DeleteEmployeeCommandRequestHandler : IRequestHandler<DeleteEmployeeCommandRequest, DeleteEmployeeCommandResponse>
    {
        private readonly IEfCoreEmployeeWriteRepository _writeRepository;
        private readonly IEfCoreEmployeeReadRepository _readRepository;
        private readonly IValidator<DeleteEmployeeCommandRequest> _validator;
        private readonly ILogger<DeleteEmployeeCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMapper _mapper;

        public DeleteEmployeeCommandRequestHandler(
            IEfCoreEmployeeWriteRepository writeRepository,
            IEfCoreEmployeeReadRepository readRepository,
            IValidator<DeleteEmployeeCommandRequest> validator,
            ILogger<DeleteEmployeeCommandRequestHandler> logger,
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

        public async Task<DeleteEmployeeCommandResponse> Handle(DeleteEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteEmployeeCommandResponse();
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                return new DeleteEmployeeCommandResponse();
            }

            var employeeEntity = _mapper.Map<EfEntity.Employee>(request);

            _writeRepository.Delete(employeeEntity);
            _writeRepository.SaveChanges();

            var employeeDeletedEvent = _mapper.Map<EmployeeDeletedEvent>(employeeEntity);

            _messageService.SendEventQueue<EmployeeDeletedEvent>(
                exchangeName: RabbitMQExchanges.EMPLOYEE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.EMPLOYEE_DELETED_ROUTING_KEY,
                @event: employeeDeletedEvent);

            return new DeleteEmployeeCommandResponse();
        }
    }
}
