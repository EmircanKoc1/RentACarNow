using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.UpdateRental
{
    public class UpdateRentalCommandRequestHandler : IRequestHandler<UpdateRentalCommandRequest, UpdateRentalCommandResponse>
    {

        private readonly IRentalUnitOfWork _rentalUnitOfWork;
        private readonly IRentalOutboxRepository _rentalOutboxRepository;
        private readonly IValidator<UpdateRentalCommandRequest> _validator;
        private readonly ILogger<UpdateRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateRentalCommandRequestHandler(
            IRentalUnitOfWork rentalUnitOfWork,
            IRentalOutboxRepository rentalOutboxRepository,
            IValidator<UpdateRentalCommandRequest> validator,
            ILogger<UpdateRentalCommandRequestHandler> logger,
            IMapper mapper)
        {
            _rentalUnitOfWork = rentalUnitOfWork;
            _rentalOutboxRepository = rentalOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UpdateRentalCommandResponse> Handle(UpdateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(UpdateCarCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(UpdateRentalCommandRequestHandler)} Request not validated");

                return new UpdateRentalCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };

            }
            var rentalExists = await _rentalUnitOfWork.RentalReadRepository.IsExistsAsync(request.RentalId);
            var foundedUser = await _rentalUnitOfWork.UserReadRepository.GetByIdAsync(request.UserId);
            var foundedCar = await _rentalUnitOfWork.CarReadRepository.GetByIdAsync(request.CarId);

            if (!rentalExists || foundedCar is null || foundedCar is null)
            {
                _logger.LogInformation($"{nameof(UpdateRentalCommandRequestHandler)} Entities not found , rental id : {request.RentalId} ,  car id : {request.CarId} ,  user id : {request.UserId}");

                return new UpdateRentalCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "rental or user or car not found",
                            PropertyName = null
                        }
                    }
                };

            }


            var efRentalEntity = _mapper.Map<EfEntity.Rental>(request);
            efRentalEntity.UpdatedDate = DateHelper.GetDate();
            

            var rentalUpdatedEvent = _mapper.Map<RentalCreatedEvent>(efRentalEntity);
            rentalUpdatedEvent.Car = _mapper.Map<CarMessage>(foundedCar);
            rentalUpdatedEvent.User = _mapper.Map<UserMessage>(foundedUser);
            rentalUpdatedEvent.UpdatedDate = DateHelper.GetDate();


            using var mongoSession = await _rentalOutboxRepository.StartSessionAsync();
            try
            {
                mongoSession.StartTransaction();
                await _rentalUnitOfWork.BeginTransactionAsync();

                await _rentalUnitOfWork.RentalWriteRepository.UpdateAsync(efRentalEntity);

                var outboxMessage = new RentalOutboxMessage()
                {
                    Id = rentalUpdatedEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
                    EventType = RentalEventType.RentalUpdatedEvent,
                    Payload = rentalUpdatedEvent.Serialize()!
                };


                await _rentalOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await _rentalUnitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _rentalUnitOfWork.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(UpdateRentalCommandRequestHandler)} transaction rollbacked");

                return new UpdateRentalCommandResponse
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

            return new UpdateRentalCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }

}
