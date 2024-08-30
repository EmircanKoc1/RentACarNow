using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequestHandler : IRequestHandler<DeleteRentalCommandRequest, DeleteRentalCommandResponse>
    {
        private readonly IRentalUnitOfWork _rentalUnitOfWork;
        private readonly IRentalOutboxRepository _rentalOutboxRepository;
        private readonly IValidator<DeleteRentalCommandRequest> _validator;
        private readonly ILogger<DeleteRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteRentalCommandRequestHandler(
            IRentalUnitOfWork rentalUnitOfWork,
            IValidator<DeleteRentalCommandRequest> validator,
            ILogger<DeleteRentalCommandRequestHandler> logger,
            IMapper mapper,
            IRentalOutboxRepository rentalOutboxRepository)
        {
            _rentalUnitOfWork = rentalUnitOfWork;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _rentalOutboxRepository = rentalOutboxRepository;
        }

        public async Task<DeleteRentalCommandResponse> Handle(DeleteRentalCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteRentalCommandResponse();
            }


            var isExists = await _rentalUnitOfWork.RentalReadRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                return new DeleteRentalCommandResponse();
            }

            var rentalEntity = _mapper.Map<EfEntity.Rental>(request);
            var rentalDeletedEvent = new RentalDeletedEvent
            {
                Id = request.Id,
            };

            using var mongoSession = await _rentalOutboxRepository.StartSessionAsync();
            try
            {
                mongoSession.StartTransaction();
                _rentalUnitOfWork.BeginTransaction();


                _rentalUnitOfWork.RentalWriteRepository.Delete(rentalEntity);
                await _rentalOutboxRepository.AddMessageAsync(new RentalOutboxMessage()
                {
                    Id = Guid.NewGuid(),
                    AddedDate = DateTime.Now,
                    EventType = RentalEventType.RentalDeletedEvent,
                    Payload = rentalDeletedEvent.Serialize()!
                }, mongoSession);


                await mongoSession.CommitTransactionAsync();
                _rentalUnitOfWork.Commit();
            }
            catch
            {
                await mongoSession.AbortTransactionAsync();
                _rentalUnitOfWork.Rollback();
                throw;
            }


            return new DeleteRentalCommandResponse();
        }
    }
}
