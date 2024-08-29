using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.Common.Events.Rental;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequestHandler : IRequestHandler<DeleteRentalCommandRequest, DeleteRentalCommandResponse>
    {
        private readonly IRentalUnitOfWork _rentalUnitOfWork;
        private readonly IValidator<DeleteRentalCommandRequest> _validator;
        private readonly ILogger<DeleteRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteRentalCommandRequestHandler(
            IRentalUnitOfWork rentalUnitOfWork,
            IValidator<DeleteRentalCommandRequest> validator,
            ILogger<DeleteRentalCommandRequestHandler> logger,
            IMapper mapper)
        {
            _rentalUnitOfWork = rentalUnitOfWork;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
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

            _rentalUnitOfWork.BeginTransaction();

            _rentalUnitOfWork.RentalWriteRepository.Delete(rentalEntity);

            _rentalUnitOfWork.Commit();


            var rentalDeletedEvent = _mapper.Map<RentalDeletedEvent>(rentalEntity);


            return new DeleteRentalCommandResponse();
        }
    }
}
