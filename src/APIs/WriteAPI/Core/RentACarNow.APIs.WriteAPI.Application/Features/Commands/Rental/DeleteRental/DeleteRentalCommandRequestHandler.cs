using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequestHandler : IRequestHandler<DeleteRentalCommandRequest, DeleteRentalCommandResponse>
    {
        private readonly IEfCoreRentalWriteRepository _writeRepository;
        private readonly IEfCoreRentalReadRepository _readRepository;
        private readonly IValidator<DeleteRentalCommandRequest> _validator;
        private readonly ILogger<DeleteRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteRentalCommandRequestHandler(
            IEfCoreRentalWriteRepository writeRepository,
            IEfCoreRentalReadRepository readRepository,
            IValidator<DeleteRentalCommandRequest> validator,
            ILogger<DeleteRentalCommandRequestHandler> logger,
            IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
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

            var isExists = await _readRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                return new DeleteRentalCommandResponse();
            }

            var rentalEntity = _mapper.Map<EfEntity.Rental>(request);

             _writeRepository.Delete(rentalEntity);
             _writeRepository.SaveChanges();

            var rentalDeletedEvent = _mapper.Map<RentalDeletedEvent>(rentalEntity);


            return new DeleteRentalCommandResponse();
        }
    }
}
