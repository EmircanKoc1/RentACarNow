using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar
{
    public class DeleteCarCommandRequestHandler : IRequestHandler<DeleteCarCommandRequest, DeleteCarCommandResponse>
    {
        private readonly IEfCoreCarWriteRepository _carWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly IValidator<DeleteCarCommandRequest> _validator;
        private readonly ILogger<DeleteCarCommandRequestHandler> _logger;
        private readonly IGuidService _guidService;
        private readonly IDateService _dateseService;
        private readonly ICarEventFactory _carEventFactory;

        public DeleteCarCommandRequestHandler(
            IEfCoreCarWriteRepository carWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            IValidator<DeleteCarCommandRequest> validator,
            ILogger<DeleteCarCommandRequestHandler> logger,
            IGuidService guidService,
            IDateService dateseService,
            ICarEventFactory carEventFactory)
        {
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
            _carOutboxRepository = carOutboxRepository;
            _validator = validator;
            _logger = logger;
            _guidService = guidService;
            _dateseService = dateseService;
            _carEventFactory = carEventFactory;
        }

        public async Task<DeleteCarCommandResponse> Handle(DeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(DeleteCarCommandRequestHandler)} Handle method has been executed");



            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(DeleteCarCommandRequestHandler)} Request not validated");


                return new DeleteCarCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var isExists = await _carReadRepository.IsExistsAsync(request.CarId);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(DeleteCarCommandRequestHandler)} car not found , id : {request.CarId}");
                return new DeleteCarCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "car not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var generatedDeletedDate = _dateseService.GetDate();
            var generatedMessageAddedDate = _dateseService.GetDate();

            var generatedMessageId = _guidService.CreateGuid();


            var carDeletedEvent = _carEventFactory.CreateCarDeletedEvent(
                carId: request.CarId,
                deletedDate: generatedDeletedDate).SetMessageId<CarDeletedEvent>(generatedMessageId);



            using var efTransaction = await _carWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();




            try
            {
                mongoSession.StartTransaction();

                _carWriteRepository.DeleteById(request.CarId);
                _carWriteRepository.SaveChanges();


                var outboxMessage = new CarOutboxMessage()
                {
                    Id = request.CarId,
                    AddedDate = generatedMessageAddedDate,
                    CarEventType = CarEventType.CarDeletedEvent,
                    Payload = carDeletedEvent.Serialize()!

                };

                await _carOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);


                await mongoSession.CommitTransactionAsync();
                await efTransaction.CommitAsync();

                _logger.LogInformation($"{nameof(DeleteCarCommandRequestHandler)} Transaction commited");
            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTransaction.RollbackAsync();

                _logger.LogError($"{nameof(DeleteCarCommandRequestHandler)} transaction rollbacked");

                return new DeleteCarCommandResponse
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

            return new DeleteCarCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
