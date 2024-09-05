using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar;
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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureDeleteCar
{
    public class FeatureDeleteCarCommandHandler : IRequestHandler<FeatureDeleteCarCommandRequest, FeatureDeleteCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureDeleteCarCommandHandler> _logger;
        private readonly IValidator<FeatureDeleteCarCommandRequest> _validator;
        private readonly IMapper _mapper;

        public FeatureDeleteCarCommandHandler(IEfCoreFeatureReadRepository featureReadRepository,
            IEfCoreFeatureWriteRepository featureWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            ILogger<FeatureDeleteCarCommandHandler> logger,
            IValidator<FeatureDeleteCarCommandRequest> validator,
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


        public async Task<FeatureDeleteCarCommandResponse> Handle(FeatureDeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(FeatureDeleteCarCommandHandler)} Handle method has been executed");

            var validationResult = await _validator.ValidateAsync(request);



            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(FeatureDeleteCarCommandHandler)} Request not validated");


                return new FeatureDeleteCarCommandResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var FeatureIsExists = await _carReadRepository.IsExistsAsync(request.FeatureId);

            if (!FeatureIsExists)
            {
                _logger.LogInformation($"{nameof(FeatureDeleteCarCommandHandler)} feature not found , id : {request.FeatureId}");
                return new FeatureDeleteCarCommandResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "feature not found",
                            PropertyName = null
                        }
                    }
                };
            }


            var efEntity = _mapper.Map<EfEntity.Feature>(request);


            using var mongoSession = await _carOutboxRepository.StartSessionAsync();
            using var efTransaction = _featureWriteRepository.BeginTransaction();

            var featureDeletedEvent = new FeatureDeletedEvent
            {
                CarId = request.CarId,
                FeatureId = request.FeatureId,
                DeletedDate = DateHelper.GetDate(),
                MessageId = Guid.NewGuid()
            };



            try
            {
                mongoSession.StartTransaction();

                _featureWriteRepository.Delete(efEntity);
                await _featureWriteRepository.SaveChangesAsync();


                var outboxMessage = new CarOutboxMessage
                {
                    Id = featureDeletedEvent.MessageId,
                    AddedDate = DateHelper.GetDate(),
                    CarEventType = CarEventType.CarFeatureDeletedEvent,
                    Payload = featureDeletedEvent.Serialize()!
                };


                await _carOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                efTransaction.Commit();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(FeatureDeleteCarCommandHandler)} Transaction commited");
            }
            catch (Exception)
            {
                efTransaction.Rollback();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(FeatureDeleteCarCommandHandler)} transaction rollbacked");

                return new FeatureDeleteCarCommandResponse
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

            return new FeatureDeleteCarCommandResponse
            {
                StatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
