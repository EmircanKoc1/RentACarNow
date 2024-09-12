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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand
{
    public class UpdateBrandCommandRequestHandler : IRequestHandler<UpdateBrandCommandRequest, UpdateBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _brandWriteRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly IValidator<UpdateBrandCommandRequest> _validator;
        private readonly ILogger<UpdateBrandCommandRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IBrandEventFactory _brandEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public UpdateBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository brandWriteRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            IBrandOutboxRepository brandOutboxRepository,
            IValidator<UpdateBrandCommandRequest> validator,
            ILogger<UpdateBrandCommandRequestHandler> logger,
            IMapper mapper,
            IBrandEventFactory brandEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _brandOutboxRepository = brandOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _brandEventFactory = brandEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<UpdateBrandCommandResponse> Handle(UpdateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(UpdateBrandCommandRequestHandler)} Handle method has been executed");

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(UpdateBrandCommandRequestHandler)} Request not validated");

                return new UpdateBrandCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };

            }

            var isExists = await _brandReadRepository.IsExistsAsync(request.Id);

            if (!isExists)
            {
                _logger.LogInformation($"{nameof(UpdateBrandCommandRequestHandler)} Entity not found , id : {request.Id}");
                return new UpdateBrandCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "Entity not found",
                            PropertyName = null
                        }
}
                };
            }


            var generatedUpdatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();

            var brandEntity = _mapper.Map<EfEntity.Brand>(request);


            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();

            var brandUpdatedEvent = _brandEventFactory.CreateBrandUpdatedEvent(
                brandId: request.Id,
                name: request.Name,
                description: request.Description,
                updatedDate: generatedUpdatedDate).SetMessageId<BrandUpdatedEvent>(generatedMessageId);

            try
            {
                mongoSession.StartTransaction();

                await _brandWriteRepository.UpdateAsync(brandEntity);
                await _brandWriteRepository.SaveChangesAsync();

                var outboxMessage = new BrandOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    EventType = BrandEventType.BrandUpdatedEvent,
                    Payload = brandUpdatedEvent.Serialize()!
                };

                await _brandOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(UpdateBrandCommandRequestHandler)} Transaction commited");


            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(UpdateBrandCommandRequestHandler)} transaction rollbacked");

                return new UpdateBrandCommandResponse
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

            return new UpdateBrandCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }

}
