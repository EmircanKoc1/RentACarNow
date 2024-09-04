using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
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

        public UpdateBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository brandWriteRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            IBrandOutboxRepository brandOutboxRepository,
            IValidator<UpdateBrandCommandRequest> validator,
            ILogger<UpdateBrandCommandRequestHandler> logger,
            IMapper mapper)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _brandOutboxRepository = brandOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
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


            var brandEntity = _mapper.Map<EfEntity.Brand>(request);
            brandEntity.UpdatedDate = DateHelper.GetDate();

            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();

            var brandUpdatedEvent = _mapper.Map<BrandUpdatedEvent>(brandEntity);
           

            try
            {
                mongoSession.StartTransaction();

                await _brandWriteRepository.UpdateAsync(brandEntity);
                await _brandWriteRepository.SaveChangesAsync();

                await _brandOutboxRepository.AddMessageAsync(new BrandOutboxMessage
                {
                    AddedDate = DateHelper.GetDate(),
                    EventType = BrandEventType.BrandUpdatedEvent,
                    Id = Guid.NewGuid(),
                    Payload = brandUpdatedEvent.Serialize()!
                }, mongoSession);

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
