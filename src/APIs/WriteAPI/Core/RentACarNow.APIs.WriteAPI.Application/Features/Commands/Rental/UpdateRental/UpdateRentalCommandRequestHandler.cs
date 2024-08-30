using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.UpdateRental
{
    public class UpdateRentalCommandRequestHandler : IRequestHandler<UpdateRentalCommandRequest, UpdateRentalCommandResponse>
    {
        private readonly IEfCoreRentalWriteRepository _writeRepository;
        private readonly IEfCoreRentalReadRepository _readRepository;
        private readonly IRentalUnitOfWork _rentalUnitOfWork;
        private readonly IRentalOutboxRepository _rentalOutboxRepository;
        private readonly IValidator<UpdateRentalCommandRequest> _validator;
        private readonly ILogger<UpdateRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        public UpdateRentalCommandRequestHandler(
            IEfCoreRentalWriteRepository writeRepository,
            IEfCoreRentalReadRepository readRepository,
            IValidator<UpdateRentalCommandRequest> validator,
            ILogger<UpdateRentalCommandRequestHandler> logger,
            IMapper mapper,
            IRentalUnitOfWork rentalUnitOfWork,
            IRentalOutboxRepository rentalOutboxRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _rentalUnitOfWork = rentalUnitOfWork;
            _rentalOutboxRepository = rentalOutboxRepository;
        }

        public async Task<UpdateRentalCommandResponse> Handle(UpdateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateRentalCommandResponse { };
            }

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateRentalCommandResponse { };

            var foundedUser = await _rentalUnitOfWork.UserReadRepository.GetByIdAsync(request.UserId);
            var foundedCar = await _rentalUnitOfWork.CarReadRepository.GetByIdAsync(request.CarId);

            if (foundedCar is null || foundedUser is null)
                return new UpdateRentalCommandResponse();


            var efRentalEntity = _mapper.Map<EfEntity.Rental>(request);
            efRentalEntity.Id = Guid.NewGuid();

            var rentalUpdatedEvent = _mapper.Map<RentalCreatedEvent>(request);
            rentalUpdatedEvent.Car = _mapper.Map<CarMessage>(foundedCar);
            rentalUpdatedEvent.User = _mapper.Map<UserMessage>(foundedUser);


            using var mongoSession = await _rentalOutboxRepository.StartSessionAsync();
            try
            {
                mongoSession.StartTransaction();
                await _rentalUnitOfWork.BeginTransactionAsync();

                await _rentalUnitOfWork.RentalWriteRepository.UpdateAsync(efRentalEntity);
                await _rentalOutboxRepository.AddMessageAsync(new RentalOutboxMessage()
                {
                    Id = Guid.NewGuid(),
                    AddedDate = DateTime.UtcNow,
                    EventType = RentalEventType.RentalUpdatedEvent,
                    Payload = rentalUpdatedEvent.Serialize()!
                }, mongoSession);

                await mongoSession.CommitTransactionAsync();
                await _rentalUnitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _rentalUnitOfWork.RollbackAsync();
                await mongoSession.AbortTransactionAsync();
                throw;
            }

            return new UpdateRentalCommandResponse { };
        }
    }

}
