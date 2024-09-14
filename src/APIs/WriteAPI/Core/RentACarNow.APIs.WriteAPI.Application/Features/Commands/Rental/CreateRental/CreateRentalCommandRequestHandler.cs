using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequestHandler : IRequestHandler<CreateRentalCommandRequest, CreateRentalCommandResponse>
    {
        private readonly IRentalUnitOfWork _rentalUnitOfWork;
        private readonly IRentalOutboxRepository _rentalOutboxRepository;
        private readonly IValidator<CreateRentalCommandRequest> _validator;
        private readonly ILogger<CreateRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRentalEventFactory _rentalEventFactory;
        private readonly IGuidService _guidService;
        private readonly IDateService _dateService;

        public CreateRentalCommandRequestHandler(
            IRentalUnitOfWork rentalUnitOfWork,
            IRentalOutboxRepository rentalOutboxRepository,
            IValidator<CreateRentalCommandRequest> validator,
            ILogger<CreateRentalCommandRequestHandler> logger,
            IMapper mapper,
            IRentalEventFactory rentalEventFactory,
            IGuidService guidService,
            IDateService dateService)
        {
            _rentalUnitOfWork = rentalUnitOfWork;
            _rentalOutboxRepository = rentalOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _rentalEventFactory = rentalEventFactory;
            _guidService = guidService;
            _dateService = dateService;
        }

        public async Task<CreateRentalCommandResponse> Handle(CreateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(CreateRentalCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(CreateRentalCommandRequestHandler)} Request not validated");


                return new CreateRentalCommandResponse
                {
                    RentalId = _guidService.GetEmptyGuid(),
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }


            var foundedUser = await _rentalUnitOfWork.UserReadRepository.GetByIdAsync(request.UserId);
            var foundedCar = await _rentalUnitOfWork.CarReadRepository.GetByIdAsync(request.CarId);

            if (foundedCar is null || foundedUser is null)
            {
                _logger.LogInformation($"{nameof(CreateRentalCommandRequestHandler)} car or user not found , car id : {request.CarId} , user id : {request.UserId}");
                return new CreateRentalCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "car or user not found",
                            PropertyName = null
                        }
                    }
                };

            }

            var generatedEntityId = _guidService.CreateGuid();
            var generatedEntityCreatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();

            var rentalTime = request.RentalStartedDate - request.RentalEndDate;
            var totalRentalPrice = (decimal)(rentalTime.TotalHours * (double)foundedCar.HourlyRentalPrice);

            var efRentalEntity = _mapper.Map<EfEntity.Rental>(request);
            efRentalEntity.Id = generatedEntityId;
            efRentalEntity.CreatedDate = generatedEntityCreatedDate;
            efRentalEntity.HourlyRentalPrice = foundedCar.HourlyRentalPrice;
            efRentalEntity.TotalRentalPrice = totalRentalPrice;


            var rentalCreatedEvent = _rentalEventFactory.CreateRentalCreatedEvent(
               rentalId: generatedEntityId,
               rentalStartedDate: request.RentalStartedDate,
               rentalEndDate :request.RentalEndDate,
               hourlyRentalPrice: foundedCar.HourlyRentalPrice,
               totalRentalPrice: totalRentalPrice,
               status : RentalStatus.Active,
               carHourlyRentalPrice : foundedCar.HourlyRentalPrice,
               carId: foundedCar.Id,
               carName: foundedCar.Name,
               carModal: foundedCar.Modal,
               carTitle: foundedCar.Title,
               carKilometer: foundedCar.Kilometer,
               carDescription: foundedCar.Description,
               carColor: foundedCar.Color,
               carPassengerCapacity: foundedCar.PassengerCapacity,
               carLuggageCapacity: foundedCar.LuggageCapacity,
               carFuelConsumption: foundedCar.FuelConsumption,
               carFuelType: foundedCar.CarFuelType,
               carTransmissionType: foundedCar.TransmissionType,
               carReleaseDate: foundedCar.ReleaseDate,
               userId: foundedUser.Id,
               userName: foundedUser.Name,
               userSurname : foundedUser.Surname,
               userAge : foundedUser.Age,
               userPhoneNumber : foundedUser.PhoneNumber,
               userEmail : foundedUser.Email,
               createdDate  : generatedEntityCreatedDate).SetMessageId<RentalCreatedEvent>(generatedMessageId);


            using var mongoSession = await _rentalOutboxRepository.StartSessionAsync();
            try
            {
                mongoSession.StartTransaction();
                await _rentalUnitOfWork.BeginTransactionAsync();

                var user = await _rentalUnitOfWork.UserReadRepository.GetByIdAsync(request.UserId);
                var car = await _rentalUnitOfWork.CarReadRepository.GetByIdAsync(request.CarId);

                if (user is null || car is null)
                    throw new Exception();

                if (user.WalletBalance < totalRentalPrice)
                    throw new Exception();

                user.WalletBalance -= totalRentalPrice;

                await _rentalUnitOfWork.UserWriteRepository.UpdateAsync(user);

                await _rentalUnitOfWork.RentalWriteRepository.AddAsync(efRentalEntity);

                var outboxMessage = new RentalOutboxMessage()
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    EventType = RentalEventType.RentalCreatedEvent,
                    Payload = rentalCreatedEvent.Serialize()!
                };


                await _rentalOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await _rentalUnitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _rentalUnitOfWork.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(CreateRentalCommandRequestHandler)} transaction rollbacked");

                return new CreateRentalCommandResponse
                {

                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "Transaction exception",
                            PropertyName = null
                        }
                    }
                };


            }




            return new CreateRentalCommandResponse
            {
                RentalId = generatedEntityId,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }
}
