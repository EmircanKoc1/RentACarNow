using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequestHandler : IRequestHandler<DeleteRentalCommandRequest, DeleteRentalCommandResponse>
    {
        private readonly IEfCoreRentalWriteRepository _rentalWriteRepository;
        private readonly IEfCoreRentalReadRepository _rentalReadRepositoyr;
        private readonly IRentalOutboxRepository _rentalOutboxRepository;
        private readonly IValidator<DeleteRentalCommandRequest> _validator;
        private readonly ILogger<DeleteRentalCommandRequestHandler> _logger;
        private readonly IGuidService _guidService;
        private readonly IDateService _dateService;
        private readonly IRentalEventFactory _rentalEventFactory;

        public DeleteRentalCommandRequestHandler(
            IEfCoreRentalWriteRepository rentalWriteRepository,
            IEfCoreRentalReadRepository rentalReadRepositoyr,
            IRentalOutboxRepository rentalOutboxRepository,
            IValidator<DeleteRentalCommandRequest> validator,
            ILogger<DeleteRentalCommandRequestHandler> logger,
            IGuidService guidService,
            IDateService dateService,
            IRentalEventFactory rentalEventFactory)
        {
            _rentalWriteRepository = rentalWriteRepository;
            _rentalReadRepositoyr = rentalReadRepositoyr;
            _rentalOutboxRepository = rentalOutboxRepository;
            _validator = validator;
            _logger = logger;
            _guidService = guidService;
            _dateService = dateService;
            _rentalEventFactory = rentalEventFactory;
        }

        public async Task<DeleteRentalCommandResponse> Handle(DeleteRentalCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(DeleteCarCommandRequestHandler)} Handle method has been executed");



            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(DeleteCarCommandRequestHandler)} Request not validated");


                return new DeleteRentalCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            

            var isExists = await _rentalReadRepositoyr.IsExistsAsync(request.RentalId);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(DeleteRentalCommandRequestHandler)} rental not found , id : {request.RentalId}");
                return new DeleteRentalCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "rental not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var generatedEntityDeletedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();


            var rentalDeletedEvent = _rentalEventFactory.CreateRentalDeletedEvent(
                rentalId: request.RentalId,
                deletedDate: generatedEntityDeletedDate).SetMessageId<RentalDeletedEvent>(generatedMessageId);

            using var mongoSession = await _rentalOutboxRepository.StartSessionAsync();
            using var efTran = await _rentalWriteRepository.BeginTransactionAsync();

            try
            {
                mongoSession.StartTransaction();


                _rentalWriteRepository.DeleteById(request.RentalId);
                await _rentalWriteRepository.SaveChangesAsync();

                var outboxMessage = new RentalOutboxMessage()
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate ,
                    EventType = RentalEventType.RentalDeletedEvent,
                    Payload = rentalDeletedEvent.Serialize()!
                };

                await _rentalOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);


                await mongoSession.CommitTransactionAsync();
                await efTran.CommitAsync();
            }
            catch
            {
                await mongoSession.AbortTransactionAsync();
                await efTran.RollbackAsync();

                _logger.LogError($"{nameof(DeleteRentalCommandRequestHandler)} transaction rollbacked");

                return new DeleteRentalCommandResponse
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


            return new DeleteRentalCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
