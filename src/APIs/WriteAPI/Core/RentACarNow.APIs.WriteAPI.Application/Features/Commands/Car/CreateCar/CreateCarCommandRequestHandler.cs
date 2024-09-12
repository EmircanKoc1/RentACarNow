using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EFEntities = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar
{
    public class CreateCarCommandRequestHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _carWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly IValidator<CreateCarCommandRequest> _validator;
        private readonly ILogger<CreateCarCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICarEventFactory _carEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;


        public CreateCarCommandRequestHandler(
            IEfCoreCarWriteRepository carWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            ICarOutboxRepository carOutboxRepository,
            IValidator<CreateCarCommandRequest> validator,
            ILogger<CreateCarCommandRequestHandler> logger,
            IMapper mapper,
            IDateService dateService,
            IGuidService guidService,
            ICarEventFactory carEventFactory)
        {
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
            _brandReadRepository = brandReadRepository;
            _carOutboxRepository = carOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _dateService = dateService;
            _guidService = guidService;
            _carEventFactory = carEventFactory;
        }

        public async Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(CreateCarCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(CreateCarCommandRequestHandler)} Request not validated");


                return new CreateCarCommandResponse
                {
                    CarId = _guidService.GetEmptyGuid(),
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }
            var foundedBrand = await _brandReadRepository.GetByIdAsync(request.BrandId);

            if (foundedBrand is null)
            {
                _logger.LogInformation($"{nameof(UpdateBrandCommandRequestHandler)} brand not found , id : {request.BrandId}");
                return new CreateCarCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "brand not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var generatedCreatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedEntityId = _guidService.CreateGuid();
            var generatedMessageId = _guidService.CreateGuid();

            var efCarEntity = _mapper.Map<EFEntities.Car>(request);
            efCarEntity.Id = generatedEntityId;
            efCarEntity.CreatedDate = generatedCreatedDate;

            var carCreatedEvent = _carEventFactory.CreateCarCreatedEvent(
                carId: generatedEntityId,
                name: request.Name,
                modal: request.Modal,
                title: request.Title,
                hourlyRentalPrice: request.HourlyRentalPrice,
                kilometer: request.Kilometer,
                description: request.Description,
                color: request.Color,
                passengerCapacity: request.PassengerCapacity,
                luggageCapacity: request.LuggageCapacity,
                fuelConsumption: request.FuelConsumption,
                releaseDate: request.ReleaseDate,
                carFuelType: request.CarFuelType,
                transmissionType: request.TransmissionType,
                brandId: request.BrandId,
                brandName: foundedBrand.Name,
                brandDescription: foundedBrand.Description,
                createdDate: generatedCreatedDate).SetMessageId<CarCreatedEvent>(generatedMessageId);


            using var efTransaction = await _carWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();



            try
            {
                mongoSession.StartTransaction();



                await _carWriteRepository.AddAsync(efCarEntity);
                await _carWriteRepository.SaveChangesAsync();


                var outboxMessage = new CarOutboxMessage()
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    CarEventType = CarEventType.CarCreatedEvent,
                    Payload = carCreatedEvent.Serialize()!,

                };

                await _carOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);


                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(CreateCarCommandRequestHandler)} Transaction commited");


            }
            catch
            {

                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(CreateCarCommandRequestHandler)} transaction rollbacked");

                return new CreateCarCommandResponse
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

            return new CreateCarCommandResponse
            {
                CarId = generatedEntityId,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }
}
