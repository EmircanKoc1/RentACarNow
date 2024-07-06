using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.BrandService.Services
{
    public class BrandUpdatedBGService : BackgroundService
    {


        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoBrandWriteRepository _brandWriteRepository;
        private readonly ILogger<BrandUpdatedBGService> _logger;

        public BrandUpdatedBGService(IRabbitMQMessageService messageService, IMongoBrandWriteRepository brandWriteRepository,
          ILogger<BrandUpdatedBGService> logger)
        {
            _messageService = messageService;
            _brandWriteRepository = brandWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.BRAND_UPDATED_QUEUE,
                 message =>
                {

                    var @event = message.Deseralize<BrandUpdatedEvent>();


                    if (@event.Cars is null || @event.Cars.Count is 0)
                    {


                        _brandWriteRepository.AddAsync(new Brand
                        {

                            Id = @event.Id,
                            Name = @event.Name,
                            Description = @event.Description,
                            CreatedDate = @event.CreatedDate,
                            DeletedDate = @event.CreatedDate,
                            UpdatedDate = @event.UpdatedDate ?? DateTime.Now,
                        });


                    }
                    else if (@event.Cars.Count is 1)
                    {
                        var car = @event.Cars.Select(cm => new Car
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
                            UpdatedDate = cm.UpdatedDate

                        }).FirstOrDefault();

                        _brandWriteRepository.UpdateCarInBrand(@event.Id, car.Id, car);

                    }
                    else
                    {

                        _brandWriteRepository.UpdateWithRelationDatasAsync(new Brand
                        {
                            Id = @event.Id,
                            Name = @event.Name,
                            Description = @event.Description,
                            CreatedDate = @event.CreatedDate,
                            DeletedDate = @event.CreatedDate,
                            UpdatedDate = @event.UpdatedDate ?? DateTime.Now,
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
                                UpdatedDate = cm.UpdatedDate

                            }).ToList()


                        });



                    }


                });

            return Task.CompletedTask;



        }
    }
}
