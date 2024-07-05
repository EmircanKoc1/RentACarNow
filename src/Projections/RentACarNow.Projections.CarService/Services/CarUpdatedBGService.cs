using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.CarService.Services
{
    public class CarUpdatedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoCarWriteRepository _carWriteRepository;
        private readonly ILogger<CarUpdatedBGService> _logger;

        public CarUpdatedBGService(IRabbitMQMessageService messageService,
            IMongoCarWriteRepository carWriteRepository,
            ILogger<CarUpdatedBGService> logger)
        {
            _messageService = messageService;
            _carWriteRepository = carWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CAR_UPDATED_QUEUE,
                async message =>
                {
                    var @event = message.Deseralize<CarUpdatedEvent>();

                    if (@event.Features is null || !@event.Features.Any())
                    {

                        await _carWriteRepository.UpdateAsync(new Car
                        {

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
                            UpdatedDate = @event.UpdatedDate

                        });


                    }
                    else
                    {
                        await _carWriteRepository.UpdateWithRelationDatasAsync(new Car
                        {
                            Id = @event.Id,
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
                            Brand = new Brand
                            {
                                Id = @event.Id,
                                Name = @event.Name,
                                Description = @event.Description,
                                CreatedDate = @event.CreatedDate,
                                DeletedDate = @event.CreatedDate,
                                UpdatedDate = @event.UpdatedDate ?? DateTime.Now,
                            },
                            Features = @event.Features.Select(fm => new Feature
                            {
                                Id = fm.Id,
                                Name = fm.Name,
                                CreatedDate = fm.CreatedDate,
                                DeletedDate = fm.DeletedDate,
                                UpdatedDate = fm.UpdatedDete,
                                Value = fm.Value

                            }).ToList()


                        });



                    }






                });





            return Task.CompletedTask;
        }
    }
}
