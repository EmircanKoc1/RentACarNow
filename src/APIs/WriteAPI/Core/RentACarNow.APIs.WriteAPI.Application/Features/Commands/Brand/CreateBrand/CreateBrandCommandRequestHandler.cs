using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequestHandler : IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _brandWriteRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly IValidator<CreateBrandCommandRequest> _validator;
        private readonly ILogger<CreateBrandCommandRequestHandler> _logger;
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly IMapper _mapper;
        private readonly IBrandEventFactory _brandEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public CreateBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository brandWriteRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            IValidator<CreateBrandCommandRequest> validator,
            ILogger<CreateBrandCommandRequestHandler> logger,
            IBrandOutboxRepository brandOutboxRepository,
            IMapper mapper,
            IBrandEventFactory eventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _validator = validator;
            _logger = logger;
            _brandOutboxRepository = brandOutboxRepository;
            _mapper = mapper;
            _brandEventFactory = eventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateBrandCommandResponse
                {
                    BrandId = _guidService.GetEmptyGuid(),
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var generatedEntityId = _guidService.CreateGuid();
            var generatedMessageId = _guidService.CreateGuid();

            var generatedCreatedDate = _dateService.GetDate();
            var generatedAddedDate = _dateService.GetDate();

            var efBrandEntity = _mapper.Map<EfEntity.Brand>(request);

            efBrandEntity.Id = generatedEntityId;
            efBrandEntity.CreatedDate = generatedCreatedDate;

            var brandCreatedEvent = _brandEventFactory.CreateBrandCreatedEvent(
                brandId: generatedEntityId,
                name: request.Name,
                description: request.Description,
                createdDate: generatedCreatedDate).SetMessageId<BrandCreatedEvent>(generatedMessageId);



            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _brandWriteRepository.AddAsync(efBrandEntity);
                await _brandWriteRepository.SaveChangesAsync();

                var outboxMessage = new BrandOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedAddedDate,
                    EventType = BrandEventType.BrandAddedEvent,
                    Payload = brandCreatedEvent.Serialize()!
                };

                await _brandOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

            }
            catch (Exception)
            {
                await mongoSession.AbortTransactionAsync();
                await efTransaction.RollbackAsync();

                return new CreateBrandCommandResponse
                {
                    BrandId = _guidService.GetEmptyGuid(),
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


            return new CreateBrandCommandResponse
            {
                BrandId = generatedEntityId,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }

}
