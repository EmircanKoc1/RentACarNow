using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Employee;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Projections.EmployeeService.Services
{
    public class EmployeeUpdatedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoEmployeeWriteRepository _employeeWriteRepository;
        private readonly ILogger<EmployeeUpdatedBGService> _logger;

        public EmployeeUpdatedBGService(IRabbitMQMessageService messageService, IMongoEmployeeWriteRepository employeeWriteRepository, ILogger<EmployeeUpdatedBGService> logger)
        {
            _messageService = messageService;
            _employeeWriteRepository = employeeWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName : RabbitMQQueues.EMPLOYEE_UPDATED_QUEUE,
                async message =>
                {
                    var @event = message.Deseralize<EmployeeUpdatedEvent>();

                    if(@event.Claims is null || !@event.Claims.Any())
                    {

                        await _employeeWriteRepository.UpdateAsync(new Employee
                        {
                            Age = @event.Age,
                            Email = @event.Email,
                            Name = @event.Name,
                            Password = @event.Password,
                            PhoneNumber = @event.PhoneNumber,
                            Position = @event.Position,
                            Surname = @event.Surname,
                            Username = @event.Username,
                            UpdatedDate = @event.UpdatedDate,
                            WorkEnvironment = @event.WorkEnvironment,
                            CreatedDate = @event.CreatedDate ?? DateTime.Now,
                            DeletedDate = @event.DeletedDate,
                        });

                    }
                    else
                    {
                        await _employeeWriteRepository.UpdateWithRelationDatasAsync(new Employee
                        {
                            Age = @event.Age,
                            Email = @event.Email,
                            Name = @event.Name,
                            Password = @event.Password,
                            PhoneNumber = @event.PhoneNumber,
                            Position = @event.Position,
                            Surname = @event.Surname,
                            Username = @event.Username,
                            UpdatedDate = @event.UpdatedDate,
                            WorkEnvironment = @event.WorkEnvironment,
                            CreatedDate = @event.CreatedDate ?? DateTime.Now,
                            DeletedDate = @event.DeletedDate,
                            Claims = @event.Claims.Select(cm => new Claim
                            {
                                Id = cm.Id,
                                Key = cm.Key,
                                Value = cm.Value,
                                CreatedDate = cm.CreatedDate,
                                DeletedDate = cm.DeletedDate,
                                UpdatedDate = cm.UpdatedDate
                            }).ToList()

                        });


                    }


                });





            return Task.CompletedTask;
        }
    }
}
