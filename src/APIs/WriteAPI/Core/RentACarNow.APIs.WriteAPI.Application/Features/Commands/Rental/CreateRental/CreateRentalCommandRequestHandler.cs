using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using ZstdSharp.Unsafe;
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
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateRentalCommandResponse();
            }

            var foundedUser = await _rentalUnitOfWork.UserReadRepository.GetByIdAsync(request.UserId);
            var foundedCar = await _rentalUnitOfWork.CarReadRepository.GetByIdAsync(request.CarId);

            if (foundedCar is null || foundedUser is null)
                return new CreateRentalCommandResponse();


            var efRentalEntity = _mapper.Map<EfEntity.Rental>(request);
            efRentalEntity.Id = Guid.NewGuid();

            var rentalCreatedEvent = _mapper.Map<RentalCreatedEvent>(request);
            rentalCreatedEvent.Car = _mapper.Map<CarMessage>(foundedCar);
            rentalCreatedEvent.User = _mapper.Map<UserMessage>(foundedUser);


            using var mongoSession = await _rentalOutboxRepository.StartSessionAsync();
            try
            {
                mongoSession.StartTransaction();
                await _rentalUnitOfWork.BeginTransactionAsync();

                await _rentalUnitOfWork.RentalWriteRepository.AddAsync(efRentalEntity);
                await _rentalOutboxRepository.AddMessageAsync(new RentalOutboxMessage()
                {
                    Id = Guid.NewGuid(),
                    AddedDate = DateTime.UtcNow,
                    EventType = RentalEventType.RentalCreatedEvent,
                    Payload = rentalCreatedEvent.Serialize()!
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




            var rentalAddedEvent = _mapper.Map<RentalCreatedEvent>(efRentalEntity);

            return new CreateRentalCommandResponse();
        }
    }
}
