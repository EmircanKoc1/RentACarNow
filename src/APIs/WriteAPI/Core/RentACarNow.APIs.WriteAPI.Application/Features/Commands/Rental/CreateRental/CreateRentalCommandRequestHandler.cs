using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequestHandler : IRequestHandler<CreateRentalCommandRequest, CreateRentalCommandResponse>
    {
        private readonly IRentalUnitOfWork _rentalUnitOfWork;
        private readonly IRentalOutboxRepository _rentalOutboxRepository;
        private readonly IValidator<CreateRentalCommandRequest> _validator;
        private readonly ILogger<CreateRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRentalCommandRequestHandler(
            IRentalUnitOfWork rentakUnitOfWork,
            IValidator<CreateRentalCommandRequest> validator,
            ILogger<CreateRentalCommandRequestHandler> logger,
            IMapper mapper,
            IRentalOutboxRepository rentalOutboxRepository)
        {
            _rentalUnitOfWork = rentakUnitOfWork;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _rentalOutboxRepository = rentalOutboxRepository;
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
                    RentalId = Guid.Empty,
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


            var efRentalEntity = _mapper.Map<EfEntity.Rental>(request);
            efRentalEntity.Id = Guid.NewGuid();
            efRentalEntity.CreatedDate = DateHelper.GetDate();
            efRentalEntity.HourlyRentalPrice = foundedCar.HourlyRentalPrice;

            var rentalTime = request.RentalStartedDate - request.RentalEndDate;
            var totalRentalPrice = rentalTime.Value.TotalHours * (double)foundedCar.HourlyRentalPrice;

            efRentalEntity.TotalRentalPrice = (decimal)totalRentalPrice;

            var rentalCreatedEvent = _mapper.Map<RentalCreatedEvent>(efRentalEntity);
            rentalCreatedEvent.Car = _mapper.Map<CarMessage>(foundedCar);
            rentalCreatedEvent.User = _mapper.Map<UserMessage>(foundedUser);



            using var mongoSession = await _rentalOutboxRepository.StartSessionAsync();
            try
            {
                mongoSession.StartTransaction();
                await _rentalUnitOfWork.BeginTransactionAsync();

                var user = await _rentalUnitOfWork.UserReadRepository.GetByIdAsync(request.UserId);
                var car = await _rentalUnitOfWork.CarReadRepository.GetByIdAsync(request.CarId);

                if (user is null || car is null)
                    throw new Exception();

                if (user.WalletBalance < (decimal)totalRentalPrice)
                    throw new Exception();

                user.WalletBalance -= (decimal)totalRentalPrice;

                await _rentalUnitOfWork.UserWriteRepository.UpdateAsync(user);

                await _rentalUnitOfWork.RentalWriteRepository.AddAsync(efRentalEntity);

                var outboxMessage = new RentalOutboxMessage()
                {
                    Id = rentalCreatedEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
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
                RentalId = efRentalEntity.Id,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }
}
