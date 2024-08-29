using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.Common.Events.Rental;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequestHandler : IRequestHandler<CreateRentalCommandRequest, CreateRentalCommandResponse>
    {
        private readonly IRentalUnitOfWork _rentalUnitOfWork;
        private readonly IValidator<CreateRentalCommandRequest> _validator;
        private readonly ILogger<CreateRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRentalCommandRequestHandler(
            IRentalUnitOfWork rentakUnitOfWork,
            IValidator<CreateRentalCommandRequest> validator,
            ILogger<CreateRentalCommandRequestHandler> logger,
            IMapper mapper)
        {
            _rentalUnitOfWork = rentakUnitOfWork;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CreateRentalCommandResponse> Handle(CreateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateRentalCommandResponse();
            }

            var efRentalEntity = _mapper.Map<EfEntity.Rental>(request);

            efRentalEntity.Id = Guid.NewGuid();

            try
            {
                await _rentalUnitOfWork.BeginTransactionAsync();
                

                await _rentalUnitOfWork.RentalWriteRepository.AddAsync(efRentalEntity);


                await _rentalUnitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _rentalUnitOfWork.RollbackAsync();
                throw;
            }




            var rentalAddedEvent = _mapper.Map<RentalCreatedEvent>(efRentalEntity);

            return new CreateRentalCommandResponse();
        }
    }
}
