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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequestHandler : IRequestHandler<CreateRentalCommandRequest, CreateRentalCommandResponse>
    {
        private readonly IEfCoreRentalWriteRepository _writeRepository;
        private readonly IEfCoreRentalReadRepository _readRepository;
        private readonly IValidator<CreateRentalCommandRequest> _validator;
        private readonly ILogger<CreateRentalCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRentalCommandRequestHandler(
            IEfCoreRentalWriteRepository writeRepository,
            IEfCoreRentalReadRepository readRepository,
            IValidator<CreateRentalCommandRequest> validator,
            ILogger<CreateRentalCommandRequestHandler> logger,
            IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
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

            var rentalEntity = _mapper.Map<EfEntity.Rental>(request);

            await _writeRepository.AddAsync(rentalEntity);
            await _writeRepository.SaveChangesAsync();

            var rentalAddedEvent = _mapper.Map<RentalAddedEvent>(rentalEntity);

            return new CreateRentalCommandResponse();
        }
    }
}
