using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Feature;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureUpdateCar
{

    public class FeatureUpdateCarCommandHandler : IRequestHandler<FeatureUpdateCarCommandRequest, FeatureUpdateCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureUpdateCarCommandHandler> _logger;
        private readonly IValidator<FeatureUpdateCarCommandRequest> _validator;
        private readonly IMapper _mapper;

        public FeatureUpdateCarCommandHandler(
            IEfCoreFeatureReadRepository featureReadRepository,
            IEfCoreFeatureWriteRepository featureWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            ILogger<FeatureUpdateCarCommandHandler> logger,
            IValidator<FeatureUpdateCarCommandRequest> validator,
            IMapper mapper)
        {
            _featureReadRepository = featureReadRepository;
            _featureWriteRepository = featureWriteRepository;
            _carReadRepository = carReadRepository;
            _carOutboxRepository = carOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<FeatureUpdateCarCommandResponse> Handle(FeatureUpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(FeatureUpdateCarCommandHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(FeatureUpdateCarCommandHandler)} Request not validated");

                return new FeatureUpdateCarCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };

            }

            var isExistFeature = await _featureReadRepository.IsExistsAsync(request.FeatureId);


            if (!isExistFeature)
            {
                _logger.LogInformation($"{nameof(FeatureUpdateCarCommandHandler)} Entity not found , id : {request.FeatureId}");

                return new FeatureUpdateCarCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "feature or car  not found",
                            PropertyName = null
                        }
}
                };


            }
            var efEntity = _mapper.Map<EfEntity.Feature>(request);

            using var efTransaction = await _featureWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();

            var featureUpdatedCarEvent = _mapper.Map<FeatureUpdatedEvent>(efEntity);

            featureUpdatedCarEvent.UpdatedDate = DateHelper.GetDate();

            try
            {
                mongoSession.StartTransaction();

                await _featureWriteRepository.UpdateAsync(efEntity);
                await _featureWriteRepository.SaveChangesAsync();

                var outboxMessage = new CarOutboxMessage
                {
                    Id = featureUpdatedCarEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
                    CarEventType = CarEventType.CarFeatureUpdatedEvent,
                    Payload = featureUpdatedCarEvent.Serialize()!
                };

                await _carOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(FeatureUpdateCarCommandHandler)} transaction rollbacked");

                return new FeatureUpdateCarCommandResponse
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

            return new FeatureUpdateCarCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
