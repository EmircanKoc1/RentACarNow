using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.BrandService.Services
{
    public class BrandAddedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoBrandWriteRepository _brandWriteRepository;
        private readonly ILogger<BrandAddedBGService> _logger;

        public BrandAddedBGService(IRabbitMQMessageService messageService, IMongoBrandWriteRepository brandWriteRepository, ILogger<BrandAddedBGService> logger)
        {
            _messageService = messageService;
            _brandWriteRepository = brandWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.BRAND_ADDED_QUEUE,
                async (message) =>
                {

                    var @event = message.Deseralize<BrandAddedEvent>();

                    await _brandWriteRepository.AddAsync(new Brand
                    {
                        Name = @event.Name,
                        Description = @event.Description,
                        CreatedDate = DateTime.Now,
                        DeletedDate = null,
                        UpdatedDate = null,
                        Cars = @event.Cars.Select(cm => new Car
                        {
                            CarFuelType = cm.CarFuelType,
                            Color = cm.Color,
                            Description = cm.Description,
                            FuelConsumption = cm.FuelConsumption,
                            HourlyRentalPrice = cm.HourlyRentalPrice,
                            Kilometer = cm.Kilometer,
                            LuggageCapacity = cm.LuggageCapacity,
                            Modal = cm.Modal,
                            Name = cm.Name,
                            PassengerCapacity = cm.PassengerCapacity,
                            TransmissionType = cm.TransmissionType,
                            ReleaseDate = cm.ReleaseDate,
                            Title = cm.Title,
                            CreatedDate = cm.CreatedDate ?? DateTime.Now,
                            DeletedDate = cm.DeletedDate,
                            UpdatedDate = cm.UpdatedDete

                        }).ToList()

                    });


                });


            return Task.CompletedTask;
        }
    }
}
