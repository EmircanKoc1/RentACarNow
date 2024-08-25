using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequestHandler : IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _brandWriteRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly IValidator<CreateBrandCommandRequest> _validator;
        private readonly ILogger<CreateBrandCommandRequestHandler> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandRequestHandler(IEfCoreBrandWriteRepository brandWriteRepository, IEfCoreBrandReadRepository brandReadRepository, IValidator<CreateBrandCommandRequest> validator, ILogger<CreateBrandCommandRequestHandler> logger, IRabbitMQMessageService messageService, IBrandOutboxRepository brandOutboxRepository, IMapper mapper)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _validator = validator;
            _logger = logger;
            _messageService = messageService;
            _brandOutboxRepository = brandOutboxRepository;
            _mapper = mapper;
        }

        public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateBrandCommandResponse { };
            }

            var efBrandEntity = _mapper.Map<EfEntity.Brand>(request);

            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();


            var brandCreatedEvent = _mapper.Map<BrandCreatedEvent>(request);

            try
            {
                mongoSession.StartTransaction();

                await _brandWriteRepository.AddAsync(efBrandEntity);
                await _brandWriteRepository.SaveChangesAsync();

                await _brandOutboxRepository.AddMessageAsync(new BrandOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsPublished = true,
                    PublishDate = null,
                    Payload = brandCreatedEvent.Serialize()
                }, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTransaction.RollbackAsync();
                throw;
            }


            return new CreateBrandCommandResponse { };
        }
    }

}
