using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.CarService.Services
{
    public class CarAddedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoCarWriteRepository _carWriteRepository;
        private readonly ILogger<CarAddedBGService> _logger;

        public CarAddedBGService(IRabbitMQMessageService messageService,
            IMongoCarWriteRepository carWriteRepository,
            ILogger<CarAddedBGService> logger)
        {
            _messageService = messageService;
            _carWriteRepository = carWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CAR_ADDED_QUEUE,
                async message =>
                {
                    var @event = message.Deseralize<CarAddedEvent>();


                    await _carWriteRepository.AddAsync(new Car
                    {
                        Id = @event.Id,

                        Brand = new Brand
                        {
                            Id = @event.Brand.Id,
                            Description = @event.Description,
                            Name = @event.Name,
                            CreatedDate = @event.CreatedDate ?? DateTime.Now,
                            DeletedDate = @event.DeletedDate,
                            UpdatedDate = @event.UpdatedDate

                        },
                        CarFuelType = @event.CarFuelType,
                        Color = @event.Color,
                        Description = @event.Description,
                        FuelConsumption = @event.FuelConsumption,
                        HourlyRentalPrice = @event.HourlyRentalPrice,
                        Kilometer = @event.Kilometer,
                        LuggageCapacity = @event.LuggageCapacity,
                        Modal = @event.Modal,
                        Name = @event.Name,
                        PassengerCapacity = @event.PassengerCapacity,
                        TransmissionType = @event.TransmissionType,
                        ReleaseDate = @event.ReleaseDate,
                        Title = @event.Title,
                        CreatedDate = @event.CreatedDate ?? DateTime.Now,
                        DeletedDate = @event.DeletedDate,
                        UpdatedDate = @event.UpdatedDate,
                        Features = @event.Features.Select(fm => new Feature
                        {
                            Id = fm.Id,
                            Value = fm.Value,
                            Name = fm.Name, 
                            CreatedDate = fm.CreatedDate,
                            UpdatedDate = fm.UpdatedDete,
                            DeletedDate = fm.DeletedDate
                            
                        }).ToList()

                    });;




                });



            return Task.CompletedTask;


        }
    }
}
