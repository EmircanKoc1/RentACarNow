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
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar
{
    public class UpdateCarCommandRequestHandler : IRequestHandler<UpdateCarCommandRequest, UpdateCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _carWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly ICarOutboxRepository _carOutboxReadRepository;
        private readonly IValidator<UpdateCarCommandRequest> _validator;
        private readonly ILogger<UpdateCarCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICarEventFactory _carEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;


        public UpdateCarCommandRequestHandler(
            IEfCoreCarWriteRepository carWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxReadRepository,
            IValidator<UpdateCarCommandRequest> validator,
            ILogger<UpdateCarCommandRequestHandler> logger,
            IMapper mapper,
            IEfCoreBrandReadRepository brandReadRepository,
            ICarEventFactory carEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
            _carOutboxReadRepository = carOutboxReadRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _brandReadRepository = brandReadRepository;
            _carEventFactory = carEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<UpdateCarCommandResponse> Handle(UpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(UpdateCarCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(UpdateBrandCommandRequestHandler)} Request not validated");

                return new UpdateCarCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };

            }




            var carIsExists = await _carReadRepository.IsExistsAsync(request.Id);
            var foundedBrand = await _brandReadRepository.GetByIdAsync(request.BrandId);
            if (!carIsExists || foundedBrand is null)
            {
                _logger.LogInformation($"{nameof(UpdateCarCommandRequestHandler)} Entity not found , id : {request.Id}");

                return new UpdateCarCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "brand or car  not found",
                            PropertyName = null
                        }
}
                };


            }

            var generatedUpdatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();



            var carEntity = _mapper.Map<EfEntity.Car>(request);
            carEntity.UpdatedDate = generatedUpdatedDate;
            

            var carUpdatedEvent = _carEventFactory.CreateCarUpdatedEvent(
                carId: request.Id,
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
                brandId: foundedBrand.Id,
                brandName: foundedBrand.Name,
                brandDescription: foundedBrand.Description,
                updatedDate: generatedUpdatedDate).SetMessageId<CarUpdatedEvent>(generatedMessageId);



            using var efTransaction = await _carWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxReadRepository.StartSessionAsync();

            try
            {

                mongoSession.StartTransaction();

                await _carWriteRepository.UpdateAsync(carEntity);
                await _carWriteRepository.SaveChangesAsync();

                var outboxMessage = new CarOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    CarEventType = CarEventType.CarUpdatedEvent,
                    Payload = carUpdatedEvent.Serialize()!
                };

                await _carOutboxReadRepository.AddMessageAsync(outboxMessage, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();


                _logger.LogInformation($"{nameof(UpdateBrandCommandRequestHandler)} Transaction commited");


            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(UpdateCarCommandRequestHandler)} transaction rollbacked");

                return new UpdateCarCommandResponse
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


            return new UpdateCarCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };

        }

    }

}
