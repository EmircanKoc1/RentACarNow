﻿
using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.RentalService.Services
{
    public class RentalUpdatedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoRentalWriteRepository _rentalWriteRepository;
        private readonly ILogger<RentalUpdatedBGService> _logger;

        public RentalUpdatedBGService(IRabbitMQMessageService messageService, IMongoRentalWriteRepository rentalWriteRepository, ILogger<RentalUpdatedBGService> logger)
        {
            _messageService = messageService;
            _rentalWriteRepository = rentalWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.RENTAL_UPDATED_QUEUE,
                 message =>
                {
                    var @event = message.Deseralize<RentalUpdatedEvent>();

                    if (@event.Car is null || @event.Customer is null)
                    {
                         _rentalWriteRepository.UpdateAsync(new Rental
                        {
                            Id = @event.Id,
                            HourlyRentalPrice = @event.HourlyRentalPrice,
                            TotalRentalPrice = @event.TotalRentalPrice,
                            RentalStartedDate = @event.RentalStartedDate,
                            RentalEndDate = @event.RentalEndDate,
                            Status = @event.Status,
                            CreatedDate = @event.CreatedDate,
                            UpdatedDate = @event.UpdatedDate,
                            DeletedDate = @event.DeletedDate,

                        });

                    }
                    else
                    {
                         _rentalWriteRepository.UpdateAsync(new Rental
                        {
                            Id = @event.Id,
                            HourlyRentalPrice = @event.HourlyRentalPrice,
                            TotalRentalPrice = @event.TotalRentalPrice,
                            RentalStartedDate = @event.RentalStartedDate,
                            RentalEndDate = @event.RentalEndDate,
                            Status = @event.Status,
                            CreatedDate = @event.CreatedDate,
                            UpdatedDate = @event.UpdatedDate,
                            DeletedDate = @event.DeletedDate,
                            Car = new Car
                            {
                                CarFuelType = @event.Car.CarFuelType,
                                Color = @event.Car.Color,
                                Description = @event.Car.Description,
                                FuelConsumption = @event.Car.FuelConsumption,
                                HourlyRentalPrice = @event.Car.HourlyRentalPrice,
                                Kilometer = @event.Car.Kilometer,
                                LuggageCapacity = @event.Car.LuggageCapacity,
                                Modal = @event.Car.Modal,
                                Name = @event.Car.Name,
                                PassengerCapacity = @event.Car.PassengerCapacity,
                                TransmissionType = @event.Car.TransmissionType,
                                ReleaseDate = @event.Car.ReleaseDate,
                                Title = @event.Car.Title,
                                CreatedDate = @event.Car.CreatedDate,
                                DeletedDate = @event.Car.DeletedDate,
                                UpdatedDate = @event.Car.UpdatedDate,
                            },
                            Customer = new Customer
                            {
                                Id = @event.Customer.Id,
                                Age = @event.Customer.Age,
                                Name = @event.Customer.Name,
                                Email = @event.Customer.Email,
                                Password = @event.Customer.Password,
                                PhoneNumber = @event.Customer.PhoneNumber,
                                Surname = @event.Customer.Surname,
                                Username = @event.Customer.Username,
                                WalletBalance = @event.Customer.WalletBalance,
                                CreatedDate = @event.Customer.CreatedDate,
                                DeletedDate = @event.Customer.DeletedDate,
                                UpdatedDate = @event.Customer.UpdatedDate,
                            }

                        });


                    }



                });



            return Task.CompletedTask;


        }
    }
}
