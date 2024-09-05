using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar;
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
namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureAddCar
{
    public class FeatureAddCarCommandHandler : IRequestHandler<FeatureAddCarCommandRequest, FeatureAddCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureAddCarCommandHandler> _logger;
        private readonly IValidator<FeatureAddCarCommandRequest> _validator;
        private readonly IMapper _mapper;

        public FeatureAddCarCommandHandler(
            IEfCoreFeatureReadRepository featureReadRepository,
            IEfCoreFeatureWriteRepository featureWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            ILogger<FeatureAddCarCommandHandler> logger,
            IValidator<FeatureAddCarCommandRequest> validator,
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

        public async Task<FeatureAddCarCommandResponse> Handle(FeatureAddCarCommandRequest request, CancellationToken cancellationToken)
        {

            _logger.LogDebug($"{nameof(FeatureAddCarCommandHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(FeatureAddCarCommandHandler)} Request not validated");


                return new FeatureAddCarCommandResponse
                {
                    CarId = request.CarId,
                    FeatureId = Guid.Empty,
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var isExists = await _carReadRepository.IsExistsAsync(request.CarId);


            if (!isExists)
            {
                _logger.LogInformation($"{nameof(FeatureAddCarCommandHandler)} brand not found , id : {request.CarId}");
                return new FeatureAddCarCommandResponse
                {
                    CarId = request.CarId,
                    FeatureId = Guid.Empty,
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "brand not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var efEntity = _mapper.Map<EfEntity.Feature>(request);

            efEntity.Id = Guid.NewGuid();

            using var efTransaction = await _featureWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();

            var featureAddedCarEvent = _mapper.Map<FeatureAddedCarEvent>(efEntity);

            featureAddedCarEvent.CreatedDate = DateHelper.GetDate();
            featureAddedCarEvent.MessageId = Guid.NewGuid();

            try
            {
                mongoSession.StartTransaction();

                await _featureWriteRepository.AddAsync(efEntity);
                await _featureWriteRepository.SaveChangesAsync();


                var outboxMessage = new CarOutboxMessage
                {
                    Id = featureAddedCarEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
                    CarEventType = CarEventType.CarFeatureAddedEvent,
                    Payload = featureAddedCarEvent.Serialize()!
                };

                await _carOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);



                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(FeatureAddCarCommandHandler)} Transaction commited");
            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(FeatureAddCarCommandHandler)} transaction rollbacked");

                return new FeatureAddCarCommandResponse
                {
                    CarId = request.CarId,
                    FeatureId = Guid.Empty,
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


            return new FeatureAddCarCommandResponse
            {
                CarId = request.CarId,
                FeatureId = efEntity.Id,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }
}
